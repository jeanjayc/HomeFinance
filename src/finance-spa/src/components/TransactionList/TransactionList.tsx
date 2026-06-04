import { useMemo } from "react";
import TransactionItem from "../TransactionItem/TransactionItem";
import InstallmentGroup from "../InstallmentGroup/InstallmentGroup";
import { type Transaction } from "../../types/Transaction";
import { buildTransactionListItems } from "../../utils/installmentGrouping";
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
  const listItems = useMemo(
    () => buildTransactionListItems(transactions),
    [transactions]
  );

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
        {listItems.map((item) => {
          if (item.kind === "installmentGroup") {
            return (
              <InstallmentGroup
                key={item.templateId}
                installments={item.installments}
                onTogglePaid={onTogglePaid}
                onDelete={handleDelete}
              />
            );
          }

          const transaction = item.transaction;
          return (
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
                  transaction.title ||
                    transaction.description ||
                    "Sem título"
                )
              }
            />
          );
        })}
      </div>
    </div>
  );
}

export default TransactionList;
