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
            var viewModel = new TransactionViewModel
            {
                Transactions = _appDbContext.Transactions.ToList(),
                Categories = _appDbContext.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new TransactionViewModel
            {
                Transactions = _appDbContext.Transactions.ToList(),
                Categories = _appDbContext.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(TransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var transaction = new Transaction
                {
                    Category = viewModel.Category,
                    Type = viewModel.Type,
                    Cost = viewModel.Cost,
                    Date = viewModel.Date,
                    Description = viewModel.Description
                };

                _appDbContext.Transactions.Add(transaction);
                _appDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            viewModel.Categories = _appDbContext.Categories.ToList();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var data = _appDbContext.Transactions.FirstOrDefault(x => x.Id == Id);

            var viewModel = new TransactionViewModel
            {
                Id = data.Id,
                Type = data.Type,
                Cost = data.Cost,
                Date = data.Date,
                Description = data.Description,
                Transactions = _appDbContext.Transactions.ToList(),
                Categories = _appDbContext.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(TransactionViewModel model)
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

            var viewModel = new TransactionViewModel
            {
                Transactions = _appDbContext.Transactions.Where(t => t.Category == tCategory).ToList(),
                Categories = _appDbContext.Categories.ToList()
            };

            return View("Index", viewModel);
        }

    }
}
