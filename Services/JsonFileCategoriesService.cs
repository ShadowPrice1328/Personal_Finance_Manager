using Microsoft.AspNetCore.Hosting;
using Personal_Finance_Manager.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Personal_Finance_Manager.Services
{
    public class JsonFileCategoriesService
    {
        private readonly AppDbContext _appDbContext;
        public JsonFileCategoriesService(IWebHostEnvironment webHostEnvironment, AppDbContext appDbContext)
        {
            WebHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public string GetCategories()
        {
            var allCategories = _appDbContext.Categories.ToList();
            return JsonSerializer.Serialize(allCategories,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                });
        }
    }
}
