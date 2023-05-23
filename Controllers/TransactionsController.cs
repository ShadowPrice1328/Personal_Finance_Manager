using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
