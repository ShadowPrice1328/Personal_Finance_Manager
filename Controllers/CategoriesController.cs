using Microsoft.AspNetCore.Mvc;

namespace Personal_Finance_Manager.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
