using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Personal_Finance_Manager.Models;

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
        public IActionResult Generate(ReportViewModel model)
        {
            bool isCategoryChosen = !string.IsNullOrEmpty(model.Category);

            if (isCategoryChosen)
            {
                return View("TableWithCategory", model);
            }
            else
            {
                return View("TableWithoutCategory", model);
            }
        }
        [HttpPost]
        public IActionResult DayByDay(ReportViewModel model)
        {
            bool isCategoryChosen = !string.IsNullOrEmpty(model.Category);

            if (isCategoryChosen)
            {
                return View("GraphWithCategory", model);
            }
            else
            {
                return View("GraphWithoutCategory", model);
            }
        }
    }
}
