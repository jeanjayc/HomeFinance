using HomeFinance.Application.Interfaces;
using HomeFinance.Domain.Models;
using HomeFinance.Infra.DTOs.Response.Financas;
using HomeFinance.Infra.Interfaces;
using HomeFinance.Infra.Interfaces.DAO;

namespace HomeFinance.Application.Services
{
    public class FinancesService : IFinancesService
    {
        private readonly IFinanceRepository _financesRepository;
        private readonly IFinancaDAO _financaDao;
        public FinancesService(IFinanceRepository financesRepository, IFinancaDAO financaDAO)
        {
            _financesRepository = financesRepository;
            _financaDao = financaDAO;
        }

        public async Task AdicionarNovasDividas(Finances finance)
        {
            try
            {
                if (finance is null) return;

                finance.FinancaId = Guid.NewGuid();
                await _financesRepository.AdicionarNovaDivida(finance);
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }

        }
        public async Task<List<Finances>> BuscarTodasFinancas()
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

        public async Task<IEnumerable<FinancaDTO>> BuscarTodasFinancasNaoPagas()
        {
            var result = await _financaDao.ObterTodasFinancasNaoPagas();
             return result;
        }
        public async Task<Finances> BuscarFinancaPorId(Guid id)
        {
            try
            {
                return await _financesRepository.ObterFinancaPorId(id);
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
                var result = await _financaDao.ObterFinancaPorDescricao(name);
                var financa = new Finances
                {
                    FinancaId = result.IdFinanca,
                    DataVencimento = result.DataVencimento,
                    Descricao = result.DescricaoFinanca,
                    Pago = result.Pago,
                    Valor = result.Valor,
                    QtdParcelas = Convert.ToInt32(result.QtdParcelas)
                };

                return financa;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Finances> AtualizarDadosFinancas(Guid id, Finances financaVM)
        {
            var financa = await BuscarFinancaPorId(id);

            if(financa is null)
            {
                throw new ArgumentNullException("Finança não encontrada");
            }

            financa.Descricao = financaVM.Descricao;
            financa.DataVencimento = financaVM.DataVencimento.ToUniversalTime();
            financa.Valor = financaVM.Valor;

            var result = await _financesRepository.AtualizarFinanca(financa);
            return result;
        }
        public async Task DeletarFinancas(Guid id)
        {
            if(id == Guid.Empty)
            {
                return;
            }
            await _financesRepository.DeletarFinanca(id);
        }

        public async Task<string> BuscarVencimentoProximo()
        {
            var todasFinancas = await BuscarTodasFinancas();
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
            var financas = await BuscarTodasFinancas();
            var somaGastos = 0m;

            foreach(var item in financas)
            {
                //somaGastos = item.Installments.Sum(fin => fin.Price);
            }

            var valorAbatidoNaRenda = renda - somaGastos;

            return valorAbatidoNaRenda;
        }



        public async Task<decimal> AlterarValorPago(Guid id)
        {
            var finances = await BuscarFinancaPorId(id);

            if(finances is null)
                throw new ArgumentNullException("Finança não encontrada");

            finances.Pago = finances.Pago is true ? finances.Pago = false : finances.Pago = true;

            await _financesRepository.AtualizarFinanca(finances);

            var totalDividas = await SomarTotalFinancas();

            var valorAtualizado = totalDividas - finances.Valor;

            return valorAtualizado;
        }

        public async Task<decimal> SomarTotalFinancas()
        {
            var todasFinancas = await BuscarFinancasNaoPagas();

            var result = 0m;

            foreach(var finance in todasFinancas)
            {
                result += finance.Valor;
            }

            return result;
        }

        private async Task<List<Finances>> BuscarFinancasNaoPagas()
        {
            try
            {
                var result = await _financesRepository.ListarTodasDividasNaoPagas();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
