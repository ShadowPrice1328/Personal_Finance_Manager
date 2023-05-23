namespace Personal_Finance_Manager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public bool Type { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
