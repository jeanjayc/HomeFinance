namespace HomeFinance.Domain.Models
{
    public class Finances
    {
        public Guid FinancesId { get; set; }
        public string FinanceName { get; set; }
        public int DueDate { get; set; }
        public decimal Price { get; set; }
    }
}
