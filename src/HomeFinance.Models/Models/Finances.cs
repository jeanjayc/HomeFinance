using System.ComponentModel.DataAnnotations.Schema;

namespace HomeFinance.Domain.Models
{
    public class Finances
    {
        [Column("id_finances")]
        public Guid FinancesId { get; set; }
        public string? FinanceName { get; set; }
        public IEnumerable<Installments> Installments { get; set; }
        public string? Owner { get; set; }
    }
}
