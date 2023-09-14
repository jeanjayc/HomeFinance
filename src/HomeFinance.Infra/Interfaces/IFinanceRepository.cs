using HomeFinance.Domain.Models;
using HomeFinance.Infra.Data;

namespace HomeFinance.Infra.Interfaces
{
    public interface IFinanceRepository
    {
        Task<Finances> AddNewFinance(Finances finance);
        Task<List<Finances>> GetAllFinances();
        Task<Finances> GetFinanceById(Guid? id);
        Task<Finances> GetFinanceByName(string name);
        Task<Finances> UpdateFinance(Finances finance);
        Task UpdatePaid(Guid? id,bool status);
        Task<Finances> DeleteFinances(Guid idFinance);
    }
}
