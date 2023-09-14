using System.ComponentModel.DataAnnotations.Schema;

namespace HomeFinance.Domain.Models
{
    public class Installments
    {
        [Column("id_installment")]
        public Guid InstallmentId { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Price { get; set; }
        public int TotalInstallments { get; set; }
        public int InstallmentsPaid { get; set; }
        public bool Paid { get; set; }
        public Finances Finance { get; set; }
        [Column("finance_id")]
        public Guid FinancesId { get; set; }
    }
}
