namespace Personal_Finance_Manager.Models
{
    public class ReportViewModel
    {
        public IEnumerable<Transaction>? Transactions { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public string Type { get; set; }
        public string? Category { get; set; }
        public ReportViewModel() 
        {
            LastDate = DateTime.Today;
        }
    }
}
