import TransactionItem from "../TransactionItem/TransactionItem";
import { type Transaction } from "../../types/Transaction";
import "./TransactionList.css";

type TransactionListProps = {
  transactions: Transaction[];
  onTogglePaid: (id: string) => void;
  onDeleteTransaction: (id: string) => Promise<void>;
};

function TransactionList({
  transactions,
  onTogglePaid,
  onDeleteTransaction,
}: TransactionListProps) {
  async function handleDelete(id: string, label: string) {
    const confirmed = window.confirm(`Excluir o lançamento "${label}"?`);
    if (!confirmed) return;
    await onDeleteTransaction(id);
  }

  return (
    <div className="transaction-list">
      <h3 className="transaction-list__title">Lançamentos</h3>

      {transactions.length === 0 && (
        <p className="transaction-list__empty">Nenhum lançamento ainda.</p>
      )}

      <div className="transaction-list__items">
        {transactions.map((transaction) => (
          <TransactionItem
            key={transaction.id}
            id={transaction.id}
            title={transaction.title}
            description={transaction.description}
            amount={transaction.amount}
            category={transaction.category}
            status={transaction.status}
            installmentNumber={transaction.installmentNumber}
            totalInstallments={transaction.totalInstallments}
            onTogglePaid={onTogglePaid}
            onDelete={() =>
              handleDelete(
                transaction.id,
                transaction.title || transaction.description || "Sem título"
              )
            }
          />
        ))}
      </div>
    </div>
  );
}

export default TransactionList;
