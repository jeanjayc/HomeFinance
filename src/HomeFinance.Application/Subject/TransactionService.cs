using HomeFinance.Application.Interfaces;
using HomeFinance.Domain.Models.Observer.Interface;
using HomeFinance.Infra.Interfaces;

namespace HomeFinance.Application.Subject
{
    public class TransactionService : ITransactionService
    {
        private readonly IFinancesService _financesService;
        private readonly IFinanceRepository _financeRepository;

        public TransactionService(IFinanceRepository financesRepository, IFinancesService financesService)
        {
            _financeRepository = financesRepository;
            _financesService = financesService;
        }

        private List<IObserver> _observers = new List<IObserver>();
        public Guid FinancaId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public int? QtdParcelas { get; set; }
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers?.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
        public async Task AlterarValorPago(Guid id)
        {
            try
            {
                var finances = await _financesService.BuscarFinancaPorId(id);

                if (finances is null)
                    throw new ArgumentNullException("Finança não encontrada");

                finances.Pago = finances.Pago is true ? finances.Pago = false : finances.Pago = true;

                await _financeRepository.AtualizarFinanca(finances);

                var totalDividas = await _financesService.SomarTotalFinancas();

                var valorAtualizado = totalDividas - finances.Valor;

                Notify();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
