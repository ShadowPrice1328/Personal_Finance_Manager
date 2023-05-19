using Microsoft.AspNetCore.Mvc;

namespace Personal_Finance_Manager.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
