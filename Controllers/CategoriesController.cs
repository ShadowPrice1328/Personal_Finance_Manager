using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Personal_Finance_Manager.Models;

namespace Personal_Finance_Manager.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoriesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Returning to Index if problem with connection
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_appDbContext.Database.CanConnect())
            {
                context.Result = RedirectToAction("Index", "Home");
            }

            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            var allCategories = _appDbContext.Categories.ToList();
            return View(allCategories);
        }

        public IActionResult Search(string cName)
        {
            // If searchbox isn't empty then starts searching
            if (!string.IsNullOrEmpty(cName))
            {
                cName = cName.Trim().ToLower();

                var searchResults = _appDbContext.Categories
                    .Where(c => c.Name.ToLower().StartsWith(cName))
                    .ToList();

                if (searchResults.Count != 0)
                {
                    TempData["AfterSearch"] = true;
                    return View("Index", searchResults);
                }
                else
                {
                    TempData["Message"] = "Nothing found!";
                }
            }

            // If searchbox is empty -> updates the page
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Categories.Add(model);
                _appDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            // Selecting needed category and transfering to POST
            var data = _appDbContext.Categories.FirstOrDefault(x => x.Id == Id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            // Selecting needed category
            var data = _appDbContext.Categories.FirstOrDefault(x => x.Id == model.Id);

            // If exists in Database:
            if (data != null)
            {
                // Looking for transaction with this Category
                var relatedTransactions = _appDbContext.Transactions
                .Where(t => t.Category == data.Name)
                .ToList();

                // Editing values
                data.Name = model.Name;
                data.Description = model.Description;

                // If transactions exist:
                if (relatedTransactions != null)
                {
                    foreach (var transaction in relatedTransactions)
                    {
                        // Change their category
                        transaction.Category = model.Name;
                    }
                }

                _appDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            // Selecting needed category
            var data = _appDbContext.Categories.FirstOrDefault(x => x.Id == Id);
            return View(data);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            // Selecting needed category and transfering to POST
            var model = _appDbContext.Categories.FirstOrDefault(x => x.Id == Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Category model)
        {
            // Removing selected in [HttpGet] category
            _appDbContext.Categories.Remove(model);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
