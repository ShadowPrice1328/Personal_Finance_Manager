using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal_Finance_Manager.Models;
using System.Diagnostics;

namespace Personal_Finance_Manager.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
            //try
            //{
            //    _dbContext.Database.OpenConnection();
            //    var isConnected = _dbContext.Database.CanConnect();
            //    _dbContext.Database.CloseConnection();

            //    if (isConnected)
            //    {
            //        ViewBag.ConnectionStatus = "Підключено до бази даних";
            //    }
            //    else
            //    {
            //        ViewBag.ConnectionStatus = "Не вдалося підключитись до бази даних";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.ConnectionStatus = "Помилка при підключенні до бази даних: " + ex.Message;
            //}

            return View();
		}

		public IActionResult Categories()
		{
			return View();
		}

        public IActionResult Transactions()
        {
            return View();
        }
        public IActionResult Reports()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}