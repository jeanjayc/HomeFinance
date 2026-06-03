
using HomeFinance.Application.Interfaces;
using HomeFinance.Application.Subject;
using HomeFinance.Domain.Models.Observer.Interface;
using HomeFinance.Infra.Interfaces;

namespace HomeFinance.Application.Observer.Handlers
{
    public class UpdateBalanceHandler : IObserver
    {
        private readonly IFinanceRepository _financesRepository;

        public UpdateBalanceHandler(IFinanceRepository financesRepository)
        {
            _financesRepository = financesRepository;
        }

        public async Task Update(ITransactionService transactionService)
        {
               // await _financesRepository.AtualizarPago((transactionService as TransactionService).FinancaId, transactionService)
        }
    }
}
