using System.ComponentModel.DataAnnotations.Schema;

namespace HomeFinance.Domain.Models
{
    public class Finances
    {
        public Finances() { }

        public Finances(string descricao, DateTime dataVencimento, decimal valor)
        {
            FinancaId = Guid.NewGuid();
            Descricao = descricao;
            DataVencimento = dataVencimento;
            Valor = valor;
        }

        [Column("idfinanca")]
        public Guid FinancaId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string? Titulo { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public int? QtdParcelas { get; set; }
        public string Categoria { get; set; } = "expense";
        public int MesReferencia { get; set; } 
        public string TipoRecorrencia { get; set; } = "single";
        public int? NumeroParcela { get; set; }
        public int? DiaVencimento { get; set; }
        public Guid? TemplateId { get; set; }
    }
}
