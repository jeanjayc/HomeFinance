namespace HomeFinance.Domain.Models.Observer.Interface
{
    public interface ITransactionService
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();
    }
}
