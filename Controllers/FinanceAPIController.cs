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
        public FinanceAPIController(FinanceService FinanceService) 
        {
            this.FinanceService = FinanceService;
        }

        public FinanceService FinanceService { get; }

        [Route("categories")]
        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return FinanceService.GetCategories();
        }

        [Route("categories/{Name}")]
        [HttpGet]
        public Category GetCategory([FromRoute] string Name)
        {
            return FinanceService.GetCategory(Name);
        }

        [Route("transactions")]
        [HttpGet]
        public IEnumerable<Transaction> GetTransactions()
        {
            return FinanceService.GetTransactions();
        }
    }
}
