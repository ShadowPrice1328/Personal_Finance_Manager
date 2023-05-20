using Microsoft.AspNetCore.Mvc;
using Personal_Finance_Manager.Models;
using System.Xml.Linq;

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
            return View(allCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Categories.Add(model);
                _appDbContext.SaveChanges();
                ViewBag.Message = "Category has been created!";

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var data = _appDbContext.Categories.FirstOrDefault(x => x.Id == Id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            var data = _appDbContext.Categories.Where(x => x.Id == model.Id).FirstOrDefault();
            if (data != null)
            {
                data.Name = model.Name;
                data.Description = model.Description;
                _appDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            var data = _appDbContext.Categories.Where(x => x.Id == Id).FirstOrDefault();
            return View(data);
        }
        public ActionResult Delete(int Id)
        {
            var data = _appDbContext.Categories.Where(x =>x.Id == Id).FirstOrDefault();
            _appDbContext.Categories.Remove(data);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
