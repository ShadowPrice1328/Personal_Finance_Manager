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
            return View();
        }
        [HttpPost]
        public IActionResult DayByDay(ReportViewModel model)
        {
            List<Transaction> allTransactions = GetTransactions(model);

            if (!string.IsNullOrEmpty(model.Category))
            {
                ReportWithCategoryViewModel viewModel = CreateReportWithCategoryViewModel(model, allTransactions);
                return View("GraphWithCategory", viewModel);
            }
            else
            {
                ReportWithoutCategoryViewModel viewModel = CreateReportWithoutCategoryViewModel(model, allTransactions);
                return View("GraphWithoutCategory", viewModel);
            }
        }

        [HttpPost]
        public IActionResult Generate(ReportViewModel model)
        {
            List<Transaction> allTransactions = GetTransactions(model);

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
            decimal allCost = allTransactions.Sum(t => t.Cost);
            var categoryCosts = CalculateCategoryCosts(allTransactions);

            var viewModel = new ReportWithoutCategoryViewModel
            {
                FirstDate = model.firstDate,
                LastDate = model.lastDate,
                Type = model.Type,
                AllTransactions = allTransactions,
                CategoryCosts = categoryCosts,
                TotalCost = allCost
            };

            return viewModel;
        }

        private ReportWithCategoryViewModel CreateReportWithCategoryViewModel(ReportViewModel model, List<Transaction> allTransactions)
        {
            List<Transaction> selectedTransactions = allTransactions
                .Where(t => t.Category == model.Category)
                .ToList();

            decimal catCost = selectedTransactions.Sum(t => t.Cost);
            var categoryCosts = CalculateCategoryCosts(allTransactions);

            var viewModel = new ReportWithCategoryViewModel
            {
                Category = model.Category,
                FirstDate = model.firstDate,
                LastDate = model.lastDate,
                Type = model.Type,
                SelectedTransactions = selectedTransactions,
                CategoryCosts = categoryCosts,
                CategoryTotalCost = catCost
            };

            return viewModel;
        }


        private List<Transaction> GetTransactions(ReportViewModel model)
        {
            return _appDbContext.Transactions
                .Where(t => t.Date >= model.firstDate &&
                            t.Date <= model.lastDate &&
                            t.Type == model.Type)
                .ToList();
        }

        private Dictionary<string, decimal> CalculateCategoryCosts(List<Transaction> transactions)
        {
            var categories = transactions
                .Select(t => t.Category)
                .Distinct()
                .ToList();

            var categoryCosts = new Dictionary<string, decimal>();

            foreach (var category in categories)
            {
                decimal cost = transactions
                    .Where(t => t.Category == category)
                    .Sum(t => t.Cost);

                categoryCosts.Add(category, cost);
            }

            return categoryCosts;
        }
    }
}
