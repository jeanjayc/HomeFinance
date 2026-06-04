using System.ComponentModel.DataAnnotations;

namespace HomeFinance.Infra.DTOs.Request.Financas
{
    public class FinancaUpdateRequest
    {
        [Required]
        public Guid IdFinanca { get; set; }

        public string? Titulo { get; set; }

        [Required]
        public string DescricaoFinanca { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }

        [Required]
        [Range(1, 12)]
        public int MesReferencia { get; set; } = DateTime.UtcNow.Month;

        [Required]
        public string Categoria { get; set; } = "expense";

        public string TipoRecorrencia { get; set; } = "single";

        public int? QtdParcelas { get; set; }

        public int? NumeroParcela { get; set; }

        public int? DiaVencimento { get; set; }

        public Guid? TemplateId { get; set; }

        public bool Pago { get; set; }
    }
}
