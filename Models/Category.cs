using System.Text.Json;

namespace Personal_Finance_Manager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Category>(this);
    }
}
