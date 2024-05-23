namespace HomeFinance.Infra.DTOs.Response.Financas
{
    public class FinancaDTO
    {
        public Guid IdFinanca { get; set; }
        public string? DescricaoFinanca { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public int? QtdParcelas { get; set; }
    }
}
