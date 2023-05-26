namespace Personal_Finance_Manager.Models
{
    internal class ReportWithoutCategoryViewModel
    {
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public string Type { get; set; }
        public List<Transaction> AllTransactions { get; set; }
        public Dictionary<string, decimal> CategoryCosts { get; set; }
        public decimal TotalCost { get; set; }
    }
}