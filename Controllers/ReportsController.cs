using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Personal_Finance_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Personal_Finance_Manager.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ReportsController(AppDbContext appDbContext)
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
            var viewModel = new ReportViewModel
            {
                Transactions = _appDbContext.Transactions.ToList(),
                Categories = _appDbContext.Categories.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult DayByDay(ReportViewModel model)
        {
            // Receiving transactions by conditions from form
            List<Transaction> allTransactions = GetTransactions(model);

            // Creating viewModel with all tables and needed transactions
            ReportWithoutCategoryViewModel viewModel = CreateReportWithoutCategoryViewModel(model, allTransactions);
            return View("Graph", viewModel);
        }

        [HttpPost]
        public IActionResult Generate(ReportViewModel model)
        {
            // Receiving transactions by conditions from form
            List<Transaction> allTransactions = GetTransactions(model);

            // Checking if Category is selected
            if (!string.IsNullOrEmpty(model.Category))
            {
                ReportWithCategoryViewModel viewModel = CreateReportWithCategoryViewModel(model, allTransactions);
                return View("TableWithCategory", viewModel);
            }
            else
            {
                ReportWithoutCategoryViewModel viewModel = CreateReportWithoutCategoryViewModel(model, allTransactions);
                return View("TableWithoutCategory", viewModel);
            }
        }

        private ReportWithoutCategoryViewModel CreateReportWithoutCategoryViewModel(ReportViewModel model, List<Transaction> allTransactions)
        {
            // Sum of all transactions
            decimal allCost = allTransactions.Sum(t => t.Cost);

            // Creating a Dictionary <Category, Cost>
            var categoryCosts = CalculateCategoryCosts(allTransactions);

            var viewModel = new ReportWithoutCategoryViewModel
            {
                FirstDate = (DateTime)model.FirstDate,
                LastDate = model.LastDate,
                Type = model.Type,
                AllTransactions = allTransactions,
                CategoryCosts = categoryCosts,
                TotalCost = allCost
            };

            return viewModel;
        }


        private ReportWithCategoryViewModel CreateReportWithCategoryViewModel(ReportViewModel model, List<Transaction> allTransactions)
        {
            // Selecting transactions with selected category
            List<Transaction> selectedTransactions = allTransactions
                .Where(t => t.Category == model.Category)
                .ToList();

            // Counting total Cost of transactions with selected category
            decimal catCost = selectedTransactions.Sum(t => t.Cost);

            // Creating a Dictionary <Category, Cost>
            var categoryCosts = CalculateCategoryCosts(allTransactions);

            var viewModel = new ReportWithCategoryViewModel
            {
                Category = model.Category,
                FirstDate = (DateTime)model.FirstDate,
                LastDate = model.LastDate,
                Type = model.Type,
                SelectedTransactions = selectedTransactions,
                CategoryCosts = categoryCosts,
                CategoryTotalCost = catCost
            };

            return viewModel;
        }
        private List<Transaction> GetTransactions(ReportViewModel model)
        {
            // Transfering transactions of the selected period of the selected Type
            return _appDbContext.Transactions
                .Where(t => t.Date >= model.FirstDate &&
                            t.Date <= model.LastDate &&
                            t.Type == model.Type)
                .ToList();
        }

        // Dictionary <Category, Cost>
        private Dictionary<string, decimal> CalculateCategoryCosts(List<Transaction> transactions)
        {
            // Selecting transactions' categories
            var categories = transactions
                .Select(t => t.Category)
                .Distinct()
                .ToList();

            var categoryCosts = new Dictionary<string, decimal>();

            // Pushing keys and values to Dictionary <Category, Cost>
            foreach (var category in categories)
            {
                // Counting total cost of each category
                decimal cost = transactions
                    .Where(t => t.Category == category)
                    .Sum(t => t.Cost);

                categoryCosts.Add(category, cost);
            }

            // Returning Dictionary of each categories' total cost
            return categoryCosts;
        }
    }
}
