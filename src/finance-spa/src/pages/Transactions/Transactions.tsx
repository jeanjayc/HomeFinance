import { type Transaction } from "../../types/Transaction";
import TransactionList from "../../components/TransactionList/TransactionList";
import PageStatus from "../../components/PageStatus/PageStatus";

type TransactionProps = {
  transactions: Transaction[];
  loading: boolean;
  error: string | null;
  onRetry: () => void;
  onTogglePaid: (id: string) => void;
  onDeleteTransaction: (id: string) => Promise<void>;
};

function Transactions({
  transactions,
  loading,
  error,
  onRetry,
  onTogglePaid,
  onDeleteTransaction,
}: TransactionProps) {
  return (
    <div className="page page--transactions">
      <h1 className="page__title">Lançamentos</h1>
      <PageStatus loading={loading} error={error} onRetry={onRetry} />
      {!loading && !error && (
        <TransactionList
          transactions={transactions}
          onTogglePaid={onTogglePaid}
          onDeleteTransaction={onDeleteTransaction}
        />
      )}
    </div>
  );
}

export default Transactions;
