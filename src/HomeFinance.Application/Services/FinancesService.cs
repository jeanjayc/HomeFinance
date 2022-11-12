using HomeFinance.Domain.Models;

namespace HomeFinance.Application.Services
{
    public class FinancesService
    {
        public decimal AdicionarValorNovaDivida(List<decimal> valorDividas)
        {
            var totalDividas = valorDividas.Sum();
            return totalDividas;
        }

        public string BuscarVencimentoProximo(List<Finances> vecimentosProximos)
        {
            var dataHJ = DateTime.Now.Day;

            var proxVencimento = vecimentosProximos.Where(div => div.DueDate - dataHJ == 1)
                .Select(div => div.FinanceName).ToList();

            if (proxVencimento.Count is 0)
                return "Não há contas com vencimento para amanhã";

            var contasProxVencimento = string.Join(",", proxVencimento);
            var venceAmanha = $"Contas a vencer amanhã: {contasProxVencimento}";

            return venceAmanha;
        }
        
        public decimal CalcularGastos(decimal renda, List<decimal> gastos)
        {
            var somaGastos = gastos.Sum();

            var valorAbatidoNaRenda = renda - somaGastos;

            return valorAbatidoNaRenda;
        }
    }
}
