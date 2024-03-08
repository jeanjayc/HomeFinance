using HomeFinance.Domain.Models;

namespace HomeFinance.Infra.Interfaces
{
    public interface IFinanceRepository
    {
        Task<Finances> AdicionarNovaDivida(Finances finance);
        Task<List<Finances>> ListarTodasDividas();
        Task<Finances> ObterFinancaPorId(Guid? id);
        Task<Finances> ObterFinancaPorNome(string name);
        Task<Finances> AtualizarFinanca(Finances finance);
        Task AtualizarPago(Guid? id,bool status);
        Task<Finances> DeletarFinanca(Guid idFinance);
    }
}
