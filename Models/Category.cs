using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Personal_Finance_Manager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Category>(this, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });
    }
}
