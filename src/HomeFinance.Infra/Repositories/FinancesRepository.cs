using HomeFinance.Domain.Models;
using HomeFinance.Infra.Data;
using HomeFinance.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeFinance.Infra.Repositories
{
    public class FinancesRepository : IFinanceRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FinancesRepository> _logger;
        public FinancesRepository(AppDbContext context,ILogger<FinancesRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Finances> AdicionarNovaDivida(Finances finance)
        {
            try
            {
                finance.DueDate = finance.DueDate.ToUniversalTime();
                await _context.AddAsync(finance);
                await _context.SaveChangesAsync();
                return finance;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao adicionar, camada repository " + $"FinancaId:{finance.FinancesId}"+ "Erro: "+ ex.Message);
                return null;
            }
        }
        public async Task<Finances> ObterFinancaPorId(Guid? id)
        {
            var result = await _context.Finances
                .FirstOrDefaultAsync(m => m.FinancesId == id);
            return result;
        }
        public async Task<Finances> ObterFinancaPorNome(string name)
        {
            var result = await _context.Finances.FindAsync(name);
            return result;
        }
        public async Task<List<Finances>> ListarTodasDividas()
        {
            return await _context.Finances.ToListAsync();
        }

        public async Task<List<Finances>> ListarTodasDividasNaoPagas()
        {
            return await _context.Finances.Where(f => f.Paid == false).ToListAsync();
        }

        public async Task<Finances> AtualizarFinanca(Finances finance)
        {
            _context.Finances.Update(finance);
            await _context.SaveChangesAsync();
            return finance;
        }

        public async Task<Finances> DeletarFinanca(Guid idFinance)
        {
            var result = await _context.Finances.FirstOrDefaultAsync(f => f.FinancesId == idFinance) ;
            _context.Finances.Remove(result);
            await _context.SaveChangesAsync();
            return result; 
        }

        public async Task AtualizarPago(Guid? idFinance, bool status)
        {
            try
            {
                var finance = await _context.Finances.FirstOrDefaultAsync(f => f.FinancesId == idFinance);
                if(finance != null)
                {
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
