using HomeFinance.Application.Interfaces;
using HomeFinance.Domain.Models;
using HomeFinance.Infra.DTOs.Request.Financas;
using HomeFinance.Infra.DTOs.Response.Financas;
using HomeFinance.Infra.Interfaces;
using HomeFinance.Infra.Interfaces.DAO;
using HomeFinance.Infra.Mappings;
using Microsoft.Extensions.Caching.Memory;

namespace HomeFinance.Application.Services
{
    public class FinancesService : IFinancesService
    {
        private readonly IFinanceRepository _financesRepository;
        private readonly IFinancaDAO _financaDao;
        private readonly IMemoryCache _memoryCache;

        public FinancesService(IFinanceRepository financesRepository, IFinancaDAO financaDAO, IMemoryCache memoryCache)
        {
            _financesRepository = financesRepository;
            _financaDao = financaDAO;
            _memoryCache = memoryCache;
        }

        public async Task AdicionarNovasDividas(Finances finance)
        {
            if (finance is null) return;

            finance.FinancaId = Guid.NewGuid();
            await _financesRepository.AdicionarNovaDivida(finance);
        }

        public async Task<FinancaDTO> CriarFinanca(FinancaCreateRequest request)
        {
            var templateId = request.TemplateId ?? Guid.NewGuid();
            var dataVencimento = FinancaMapping.ResolveDataVencimento(request);

            if (request.TipoRecorrencia == "installment" && request.QtdParcelas is > 1)
            {
                FinancaDTO? lastDto = null;
                for (var parcela = 1; parcela <= request.QtdParcelas; parcela++)
                {
                    var parcelaRequest = CloneForParcela(request, parcela, templateId, dataVencimento);
                    var entity = FinancaMapping.ToEntity(parcelaRequest, FinancaMapping.ResolveDataVencimento(parcelaRequest));
                    entity.FinancaId = Guid.NewGuid();
                    entity.TemplateId = templateId;
                    entity.NumeroParcela = parcela;
                    entity.QtdParcelas = request.QtdParcelas;
                    await _financesRepository.AdicionarNovaDivida(entity);
                    lastDto = FinancaMapping.ToDto(entity);
                }

                return lastDto!;
            }

            var finance = FinancaMapping.ToEntity(request, dataVencimento);
            finance.FinancaId = Guid.NewGuid();
            finance.TemplateId = request.TipoRecorrencia != "single" ? templateId : request.TemplateId;
            await _financesRepository.AdicionarNovaDivida(finance);
            return FinancaMapping.ToDto(finance);
        }

        public async Task<FinancaDTO?> AtualizarFinanca(FinancaUpdateRequest request)
        {
            var financa = await BuscarFinancaPorId(request.IdFinanca);
            if (financa is null)
                return null;

            financa.Titulo = request.Titulo;
            financa.Descricao = request.DescricaoFinanca;
            financa.Valor = request.Valor;
            financa.DataVencimento = request.DataVencimento == default
                ? FinancaMapping.ResolveDataVencimento(new FinancaCreateRequest
                {
                    MesReferencia = request.MesReferencia,
                    DiaVencimento = request.DiaVencimento
                })
                : request.DataVencimento.ToUniversalTime();
            financa.MesReferencia = request.MesReferencia;
            financa.Categoria = request.Categoria;
            financa.TipoRecorrencia = request.TipoRecorrencia;
            financa.QtdParcelas = request.QtdParcelas;
            financa.NumeroParcela = request.NumeroParcela;
            financa.DiaVencimento = request.DiaVencimento;
            financa.TemplateId = request.TemplateId;
            financa.Pago = request.Pago;

            var result = await _financesRepository.AtualizarFinanca(financa);
            return FinancaMapping.ToDto(result);
        }

        public async Task<IEnumerable<FinancaDTO>> BuscarTodasFinancas()
        {
            return await _financaDao.ObterTodasFinancas();
        }

        public async Task<IEnumerable<FinancaDTO>> BuscarTodasFinancasNaoPagas()
        {
            return await _financaDao.ObterTodasFinancasNaoPagas();
        }

        public async Task<Finances> BuscarFinancaPorId(Guid id)
        {
            return await _financesRepository.ObterFinancaPorId(id);
        }

        public async Task<Finances> BuscarFinancaPorNome(string name)
        {
            var result = await _financaDao.ObterFinancaPorDescricao(name);
            return MapDtoToEntity(result);
        }

        public async Task<Finances> AtualizarDadosFinancas(Guid id, Finances financaVM)
        {
            var financa = await BuscarFinancaPorId(id);

            if (financa is null)
                throw new ArgumentNullException(nameof(financa), "Finança não encontrada");

            financa.Descricao = financaVM.Descricao;
            financa.Titulo = financaVM.Titulo;
            financa.DataVencimento = financaVM.DataVencimento.ToUniversalTime();
            financa.Valor = financaVM.Valor;
            financa.Categoria = financaVM.Categoria;
            financa.MesReferencia = financaVM.MesReferencia;
            financa.TipoRecorrencia = financaVM.TipoRecorrencia;
            financa.QtdParcelas = financaVM.QtdParcelas;
            financa.NumeroParcela = financaVM.NumeroParcela;
            financa.DiaVencimento = financaVM.DiaVencimento;
            financa.TemplateId = financaVM.TemplateId;
            financa.Pago = financaVM.Pago;

            return await _financesRepository.AtualizarFinanca(financa);
        }

        public async Task DeletarFinancas(Guid id)
        {
            if (id == Guid.Empty)
                return;

            await _financesRepository.DeletarFinanca(id);
        }

        public async Task<string> BuscarVencimentoProximo()
        {
            await BuscarTodasFinancas();
            return string.Empty;
        }

        public async Task<decimal> CalcularGastos(decimal renda)
        {
            await BuscarTodasFinancas();
            return renda;
        }

        public async Task<decimal> AlterarValorPago(Guid id)
        {
            var finances = await BuscarFinancaPorId(id);

            if (finances is null)
                throw new ArgumentNullException(nameof(finances), "Finança não encontrada");

            finances.Pago = !finances.Pago;
            await _financesRepository.AtualizarFinanca(finances);

            var totalDividas = await SomarTotalFinancas();
            return totalDividas - finances.Valor;
        }

        public Task<int> DesmarcarTodasFinancasPagasAsync()
        {
            return _financesRepository.DesmarcarTodasFinancasPagasAsync();
        }

        public async Task<decimal> SomarTotalFinancas()
        {
            var todasFinancas = await BuscarFinancasNaoPagas();
            return todasFinancas.Sum(finance => finance.Valor);
        }

        private static Finances MapDtoToEntity(FinancaDTO result)
        {
            return new Finances
            {
                FinancaId = result.IdFinanca,
                Titulo = result.Titulo,
                DataVencimento = result.DataVencimento,
                Descricao = result.DescricaoFinanca ?? string.Empty,
                Pago = result.Pago,
                Valor = result.Valor,
                QtdParcelas = result.QtdParcelas,
                Categoria = result.Categoria,
                MesReferencia = result.MesReferencia,
                TipoRecorrencia = result.TipoRecorrencia,
                NumeroParcela = result.NumeroParcela,
                DiaVencimento = result.DiaVencimento,
                TemplateId = result.TemplateId
            };
        }

        private static FinancaCreateRequest CloneForParcela(
            FinancaCreateRequest request,
            int parcela,
            Guid templateId,
            DateTime baseDueDate)
        {
            var dueDate = baseDueDate.AddMonths(parcela - 1);
            return new FinancaCreateRequest
            {
                Titulo = request.Titulo,
                DescricaoFinanca = request.DescricaoFinanca,
                Valor = request.Valor,
                DataVencimento = dueDate,
                MesReferencia = dueDate.Month,
                Categoria = request.Categoria,
                TipoRecorrencia = request.TipoRecorrencia,
                QtdParcelas = request.QtdParcelas,
                NumeroParcela = parcela,
                DiaVencimento = request.DiaVencimento ?? dueDate.Day,
                TemplateId = templateId,
                Pago = request.Pago
            };
        }

        private async Task<List<Finances>> BuscarFinancasNaoPagas()
        {
            return await _financesRepository.ListarTodasDividasNaoPagas();
        }
    }
}
