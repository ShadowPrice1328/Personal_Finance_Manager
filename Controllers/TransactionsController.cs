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
            // Transfering all tables to Index
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
            // Transfering all tables to the Create form
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
            // Creates new transaction if model is valid
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

            // If model is invalid it gives next try
            viewModel.Categories = _appDbContext.Categories.ToList();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            // Selecting needed transaction
            var data = _appDbContext.Transactions.FirstOrDefault(x => x.Id == Id);

            // Transfering transaction's data to viewModel with tables included to POST
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
            // Selecting needed transaction
            var data = _appDbContext.Transactions.Where(x => x.Id == model.Id).FirstOrDefault();

            // If exists in Database:
            if (data != null)
            {
                // Editing values
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
            // Selecting needed transaction
            var data = _appDbContext.Transactions.Where(x => x.Id == Id).FirstOrDefault();
            return View(data);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            // Selecting needed transaction and transfering to POST
            Transaction? model = _appDbContext.Transactions.Where(x => x.Id == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(Transaction model)
        {
            // Removing selected in [HttpGet] transaction
            _appDbContext.Transactions.Remove(model);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // Shows transaction from selected category
        public IActionResult Filter(string tCategory)
        {
            // If nothing is selected -> updates the page
            if (tCategory == "Select Category")
            {
                return RedirectToAction("Index");
            }

            // If category is selected -> creates viewModel with all tables included
            // and transfers it to Index page where all transactions will be displayes
            var viewModel = new TransactionViewModel
            {
                Transactions = _appDbContext.Transactions.Where(t => t.Category == tCategory).ToList(),
                Categories = _appDbContext.Categories.ToList()
            };

            return View("Index", viewModel);
        }

    }
}
