using System.ComponentModel.DataAnnotations;

namespace HomeFinance.MVC.ViewModels
{
    public class FinanceVM
    {
        public Guid FinancesId { get; set; }

        [Display(Name = "Descrição da Divida")]
        public string? FinanceName { get; set; }

        [Display(Name = "Data de Vencimento")]
        public string DueDate { get; set; }

        [Display(Name = "Valor R$")]
        public decimal Price { get; set; }

        [Display(Name = "Pago")]
        public bool Paid { get; set; }

        [Display(Name = "Quantidade de Parcelas")]
        public int? QtdInstallments { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
