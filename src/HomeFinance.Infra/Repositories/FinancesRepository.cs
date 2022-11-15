using HomeFinance.Domain.Models;
using HomeFinance.Infra.Data;
using HomeFinance.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeFinance.Infra.Repositories
{
    public class FinancesRepository : IFinanceRepository
    {
        private readonly AppDbContext _context;

        public FinancesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Finances> AddNewFinance(Finances finance)
        {
            _context.AddAsync(finance);
            await _context.SaveChangesAsync();
            return finance;
        }
        public async Task<Finances> GetFinanceById(Guid? id)
        {
            var result = await _context.Finances
                .FirstOrDefaultAsync(m => m.FinancesId == id);
            return result;
        }
        public async Task<Finances> GetFinanceByName(string name)
        {
            var result = await _context.Finances.FindAsync(name);
            return result;
        }
        public async Task<List<Finances>> GetAllFinances()
        {
            return await _context.Finances.ToListAsync();
        }

        public async Task<Finances> UpdateFinance(Finances finance)
        {
            _context.Finances.Update(finance);
            await _context.SaveChangesAsync();
            return finance;
        }

        public async Task<Finances> DeleteFinances(Guid idFinance)
        {
            var result = await _context.Finances.FirstOrDefaultAsync(f => f.FinancesId == idFinance) ;
            _context.Finances.Remove(result);
            await _context.SaveChangesAsync();
            return result; 
        }

    }
}
