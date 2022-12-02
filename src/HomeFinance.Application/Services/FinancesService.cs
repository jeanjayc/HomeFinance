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
            try
            {
                if (finance is null) return;

                await _financesRepository.AddNewFinance(finance);
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }

        }
        public async Task<List<Finances>> BuscarFinancas()
        {
            try
            {
                var result = await _financesRepository.GetAllFinances();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public Task<Finances> BuscarFinancaPorId(Guid? id)
        {
            try
            {
                var result = _financesRepository.GetFinanceById(id);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Finances> BuscarFinancaPorNome(string name)
        {
            try
            {
                var result = await _financesRepository.GetFinanceByName(name);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Finances> AtualizarDadosFinancas(Finances finances)
        {
            var result = await _financesRepository.UpdateFinance(finances);
            return result;
        }
        public async Task<Finances> DeletarFinancas(Guid id)
        {
            return await _financesRepository.DeleteFinances(id);
        }

        public async Task<string> BuscarVencimentoProximo()
        {
            var todasFinancas = await BuscarFinancas();
            var dataHJ = DateTime.Now.Day;

            var proxVencimento = todasFinancas.Where(fin => Convert.ToDateTime(fin.DueDate).Date.Day - dataHJ == 1)
                .Select(div => div.FinanceName).ToList();

            if (proxVencimento.Count is 0)
                return "Não há contas com vencimento para amanhã";

            var contasProxVencimento = string.Join(",", proxVencimento);
            var venceAmanha = $"Contas a vencer amanhã: {contasProxVencimento}";

            return venceAmanha;
        }

        public async Task<decimal> CalcularGastos(decimal renda)
        {
            var financas = await BuscarFinancas();
            var somaGastos = financas.Sum(f => f.Price);

            var valorAbatidoNaRenda = renda - somaGastos;

            return valorAbatidoNaRenda;
        }

        public async Task<decimal> SomarTotalFinancas()
        {
            var todasFinancas = await BuscarFinancas();

            var result = todasFinancas.Sum(fin => fin.Price);

            return result;
        }
    }
}
