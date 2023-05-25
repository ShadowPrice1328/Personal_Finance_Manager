using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Personal_Finance_Manager.Models;

namespace Personal_Finance_Manager.Controllers
{
    public class OverviewController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public OverviewController(AppDbContext appDbContext)
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
    }
}
