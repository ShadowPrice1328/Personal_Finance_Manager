using Personal_Finance_Manager.Models;

public class TransactionViewModel
{
    public IEnumerable<Transaction>? Transactions { get ; set; }
    public IEnumerable<Category>? Categories { get; set; }
    public int Id { get; set; }
    public string Category { get; set; }
    public string Type { get; set; }
    public decimal Cost { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}
