using System.ComponentModel.DataAnnotations;

namespace HomeFinance.MVC.ViewModels
{
    public class FinanceVM
    {
        public Guid FinancaId { get; set; }

        [Display(Name = "Descrição da Divida")]
        public string? Descricao { get; set; }

        [Display(Name = "Data de Vencimento")]
        public string DataVencimento {  get; set; }

        [Display(Name = "Valor R$")]
        public decimal Valor { get; set; }

        [Display(Name = "Pago")]
        public bool Pago { get; set; }

        [Display(Name = "Quantidade de Parcelas")]
        public int? QtdParcelas { get; set; }
    }

}
