using HomeFinance.Application.Interfaces;
using HomeFinance.Domain.Models;
using HomeFinance.Infra.Dao.Interfaces.Financas;
using HomeFinance.Infra.Interfaces;

namespace HomeFinance.Application.Services
{
    public class FinancesService : IFinancesService
    {
        private readonly IFinanceRepository _financesRepository;
        private readonly IFinancaDao _financaDao;
        public FinancesService(IFinanceRepository financesRepository, IFinancaDao financaDao)
        {
            _financesRepository = financesRepository;
            _financaDao = financaDao;
        }

        public async Task AdicionarNovasDividas(Finances finance)
        {
            try
            {
                if (finance is null) return;

                finance.FinancesId = Guid.NewGuid();
                await _financesRepository.AdicionarNovaDivida(finance);
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
                var result = await _financesRepository.ListarTodasDividas();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Finances> BuscarFinancaPorId(Guid id)
        {
            try
            {
                var financa = await _financaDao.ObterPorId(id);
                return financa;
                //var result = await _financesRepository.ObterFinancaPorId(id);
                //return result;
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
                var result = await _financesRepository.ObterFinancaPorNome(name);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Finances> AtualizarDadosFinancas(Finances finances)
        {
            var result = await _financesRepository.AtualizarFinanca(finances);
            return result;
        }
        public async Task<Finances> DeletarFinancas(Guid id)
        {
            return await _financesRepository.DeletarFinanca(id);
        }

        public async Task<string> BuscarVencimentoProximo()
        {
            var todasFinancas = await BuscarFinancas();
            var dataHJ = DateTime.Now.Day;

            var proxVencimento = 0;

            //foreach(var item in todasFinancas)
            //{
            //    proxVencimento = item.Installments.Where(f => Convert.ToDateTime(f.DueDate).Day - dataHJ == 1)
            //        .Select(div => div.Finance.FinanceName).ToList();
            //}

            //if (proxVencimento.Count is 0)
            //    return "Não há contas com vencimento para amanhã";

            //var contasProxVencimento = string.Join(",", proxVencimento);
            //var venceAmanha = $"Contas a vencer amanhã: {contasProxVencimento}";

            return "";
        }

        public async Task<decimal> CalcularGastos(decimal renda)
        {
            var financas = await BuscarFinancas();
            var somaGastos = 0m;

            foreach(var item in financas)
            {
                //somaGastos = item.Installments.Sum(fin => fin.Price);
            }

            var valorAbatidoNaRenda = renda - somaGastos;

            return valorAbatidoNaRenda;
        }

        public async Task<decimal> SomarTotalFinancas()
        {
            var todasFinancas = await BuscarFinancas();

            var result = 0m;

            foreach(var finance in todasFinancas)
            {
                result += finance.Price;
            }

            return result;
        }

        public async Task<decimal> AlterarValorPago(decimal value)
        {
            var totalDividas = await SomarTotalFinancas();
            var valorAtualizado = totalDividas - value;
            return valorAtualizado;
        }
    }
}
