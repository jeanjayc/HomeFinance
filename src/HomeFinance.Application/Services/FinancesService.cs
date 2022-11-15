using HomeFinance.Application.Interfaces;
using HomeFinance.Domain.Models;
using HomeFinance.Infra.Interfaces;

namespace HomeFinance.Application.Services
{
    public class FinancesService : IFinancesService
    {
        private readonly IFinanceRepository _financesRepository;
        public FinancesService(IFinanceRepository financesRepository)
        {
            _financesRepository = financesRepository;
        }

        public async Task AdicionarNovasDividas(Finances finance)
        {
            await _financesRepository.AddNewFinance(finance);
        }
        public async Task<List<Finances>> BuscarFinancas()
        {
            return await _financesRepository.GetAllFinances();
        }
        public Task<Finances> BuscarFinancaPorId(Guid? id)
        {
            return _financesRepository.GetFinanceById(id);
        }
        public async Task<Finances> BuscarFinancaPorNome(string name)
        {
            return await _financesRepository.GetFinanceByName(name);
        }
        public async Task<Finances> AtualizarDadosFinancas(Finances finances)
        {
            return await _financesRepository.UpdateFinance(finances);
        }
        public async Task<Finances> DeletarFinancas(Guid id)
        {
            return await _financesRepository.DeleteFinances(id);
        }
        public async Task<decimal> BuscarValorTotal()
        {
            var valores = await _financesRepository.GetAllFinances();

            return valores.Sum(f => f.Price);
        }

        public async Task<string> BuscarVencimentoProximo()
        {
            var vencimentosProximos = await _financesRepository.GetAllFinances();
            var dataHJ = DateTime.Now.Day;

            var proxVencimento = vencimentosProximos.Where(div => div.DueDate.Day - dataHJ == 1)
                .Select(div => div.FinanceName).ToList();

            if (proxVencimento.Count is 0)
                return "Não há contas com vencimento para amanhã";

            var contasProxVencimento = string.Join(",", proxVencimento);
            var venceAmanha = $"Contas a vencer amanhã: {contasProxVencimento}";

            return venceAmanha;
        }

        public async Task<decimal> CalcularGastos(decimal renda)
        {
            var financas = await _financesRepository.GetAllFinances();
            var somaGastos = financas.Sum(f => f.Price);

            var valorAbatidoNaRenda = renda - somaGastos;

            return valorAbatidoNaRenda;
        }
    }
}
