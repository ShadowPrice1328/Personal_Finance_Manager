using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Personal_Finance_Manager.Models;

namespace Personal_Finance_Manager.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public TransactionsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
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
            var allTransactions = _appDbContext.Transactions.ToList();
            return View(allTransactions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var allCategories = _appDbContext.Categories.ToList();

            if (allCategories != null)
            {
                ViewData["Categories"] = allCategories;
                return View(new Transaction());
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaction model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Transactions.Add(model);
                _appDbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var data = _appDbContext.Transactions.FirstOrDefault(x => x.Id == Id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Transaction model)
        {
            var data = _appDbContext.Transactions.Where(x => x.Id == model.Id).FirstOrDefault();
            if (data != null)
            {
                data.Category = model.Category;
                data.Type = model.Type;
                data.Cost = model.Cost;
                data.Date = model.Date;
                data.Description = model.Description;

                _appDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            var data = _appDbContext.Transactions.Where(x => x.Id == Id).FirstOrDefault();
            return View(data);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Transaction? model = _appDbContext.Transactions.Where(x => x.Id == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(Transaction model)
        {
            _appDbContext.Transactions.Remove(model);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Filter(string tCategory)
        {
            if (tCategory == "Select Category")
            {
                return RedirectToAction("Index");
            }

            var filteredTransactions = _appDbContext.Transactions
                .Where(t => t.Category == tCategory)
                .ToList();

            return View("Index", filteredTransactions);
        }
    }
}
