using System.ComponentModel.DataAnnotations.Schema;

namespace HomeFinance.Domain.Models
{
    public class Finances
    {
        [Column("idfinanca")]
        public Guid FinancaId { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public int? QtdParcelas { get; set; }
    }
}
