using HomeFinance.Infra.DTOs.Response.Financas;

namespace HomeFinance.Infra.Interfaces.DAO
{
    public interface IFinancaDAO
    {
        Task<IEnumerable<FinancaDTO>> ObterTodasFinancas();
        Task<IEnumerable<FinancaDTO>> ObterTodasFinancasNaoPagas();
        Task<IEnumerable<FinancaDTO>> ObterTodasFinancasPagas();
        Task<FinancaDTO> ObterFinancaPorDescricao(string descricao);
    }
}
