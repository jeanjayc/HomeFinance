import TransactionItem from "../TransactionItem/TransactionItem";
import { type Transaction } from "../../types/Transaction";
import "./TransactionList.css";

type TransactionListProps = {
  transactions: Transaction[];
  onTogglePaid: (id: string) => void;
};

function TransactionList({ transactions, onTogglePaid }: TransactionListProps) {
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
          />
        ))}
      </div>
    </div>
  );
}

export default TransactionList;
