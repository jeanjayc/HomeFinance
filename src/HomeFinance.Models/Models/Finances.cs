using System.ComponentModel.DataAnnotations.Schema;

namespace HomeFinance.Domain.Models
{
    public class Finances
    {
        public Finances(){}
        public Finances(string descricao, DateTime dataVencimento, decimal valor)
        {
            FinancaId = Guid.NewGuid();
            Descricao = descricao;
            DataVencimento = dataVencimento;
            Valor = valor;
        }

        [Column("idfinanca")]
        public Guid FinancaId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public int? QtdParcelas { get; set; }
    }
}
