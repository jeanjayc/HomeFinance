namespace HomeFinance.Domain.Models.Observer.Interface
{
    public interface IObserver
    {
        Task Update(ITransactionService transactionService);
    }
}
