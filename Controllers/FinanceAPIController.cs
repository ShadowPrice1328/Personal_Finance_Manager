using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal_Finance_Manager.Models;
using Personal_Finance_Manager.Services;

namespace Personal_Finance_Manager.Controllers
{
    [Route("api")]
    [ApiController]
    public class FinanceAPIController : ControllerBase
    {
        public FinanceAPIController(JsonService jsonService) 
        {
            JsonService = jsonService;
        }

        public JsonService JsonService { get; }

        [Route("categories")]
        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return JsonService.GetCategories();
        }

        [Route("transactions")]
        [HttpGet]
        public IEnumerable<Transaction> GetTransactions()
        {
            return JsonService.GetTransactions();
        }
    }
}
