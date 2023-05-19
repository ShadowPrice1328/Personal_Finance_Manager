using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Personal_Finance_Manager.Models;
using System.Diagnostics;

namespace Personal_Finance_Manager.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

		public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
		{
            _appDbContext = appDbContext;
			_logger = logger;
		}

        public IActionResult Index()
        {
            try
            {
                using (_appDbContext.Database.GetDbConnection())
                {
                    _appDbContext.Database.OpenConnection();
                    var isConnected = _appDbContext.Database.CanConnect();
                    _appDbContext.Database.CloseConnection();

                    if (isConnected)
                    {
                        ViewBag.ConnectionStatus = $"Connected to [{_appDbContext.Database.GetDbConnection().Database}] database!";
                        ViewBag.ConnectionStatusColor = "text-done";
                    }
                    else
                    {
                        ViewBag.ConnectionStatus = "Not Connected!";
                        ViewBag.ConnectionStatusColor = "text-warning";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ConnectionStatus = "Connection error!";
                ViewBag.Desc = ex.Message;
                ViewBag.ConnectionStatusColor = "text-danger";
            }

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