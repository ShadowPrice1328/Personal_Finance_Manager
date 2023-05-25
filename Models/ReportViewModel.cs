namespace Personal_Finance_Manager.Models
{
    public class ReportViewModel
    {
        public DateTime firstDate { get; set; }
        public DateTime lastDate { get; set; }
        public string Type { get; set; }
        public string? Category { get; set; }
    }
}
