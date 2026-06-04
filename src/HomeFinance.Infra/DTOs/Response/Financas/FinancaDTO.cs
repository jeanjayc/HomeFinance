namespace HomeFinance.Infra.DTOs.Response.Financas
{
    public class FinancaDTO
    {
        public Guid IdFinanca { get; set; }
        public string? Titulo { get; set; }
        public string? DescricaoFinanca { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public int? QtdParcelas { get; set; }
        public string Categoria { get; set; } = "expense";
        public int MesReferencia { get; set; } = DateTime.UtcNow.Month;
        public string TipoRecorrencia { get; set; } = "single";
        public int? NumeroParcela { get; set; }
        public int? DiaVencimento { get; set; }
        public Guid? TemplateId { get; set; }
    }
}
