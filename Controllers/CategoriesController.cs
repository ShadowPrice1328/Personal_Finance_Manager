using Microsoft.AspNetCore.Mvc;
using Personal_Finance_Manager.Models;

namespace Personal_Finance_Manager.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public CategoriesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var allCategories = _appDbContext.Categories.ToList();
            var viewModel = new CategoriesViewModel(allCategories);
            return View(viewModel);
        }
    }
}
