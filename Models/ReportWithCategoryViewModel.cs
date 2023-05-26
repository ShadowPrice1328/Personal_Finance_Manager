namespace Personal_Finance_Manager.Models
{
    internal class ReportWithCategoryViewModel
    {
        public string Category { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public string Type { get; set; }
        public List<Transaction> SelectedTransactions { get; set; }
        public Dictionary<string, decimal> CategoryCosts { get; set; }
        public decimal CategoryTotalCost { get; set; }
    }
}