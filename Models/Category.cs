using System.ComponentModel.DataAnnotations;

namespace Personal_Finance_Manager.Models
{
    public class Category
    {
        [Key]
        public int Id_category { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
