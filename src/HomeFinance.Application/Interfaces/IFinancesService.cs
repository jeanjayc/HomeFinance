using HomeFinance.Domain.Models;

namespace HomeFinance.Application.Interfaces
{
    public interface IFinancesService
    {
        Task AdicionarNovasDividas(Finances finance);
        Task<Finances> AtualizarDadosFinancas(Finances finances);
        Task<Finances> BuscarFinancaPorId(Guid id);
        Task<Finances> BuscarFinancaPorNome(string name);
        Task<List<Finances>> BuscarFinancas();
        Task<string> BuscarVencimentoProximo();
        Task<decimal> CalcularGastos(decimal renda);
        Task<Finances> DeletarFinancas(Guid id);
        Task<decimal> AlterarValorPago(decimal value);
        Task<decimal> SomarTotalFinancas();
    }
}