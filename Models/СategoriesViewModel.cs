using Personal_Finance_Manager.Models;

public class CategoriesViewModel
{
    public List<Category> Categories { get; set; }

    public CategoriesViewModel(List<Category> allCategories)
    {
        Categories = allCategories;
    }
}
