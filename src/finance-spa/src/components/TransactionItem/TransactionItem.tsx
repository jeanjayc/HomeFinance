import { Link } from "react-router-dom";
import "./TransactionItem.css";

type TransactionItemProps = {
  id: string;
  title: string;
  description: string;
  amount: number;
  category: "income" | "expense";
  status: "pending" | "paid";
  installmentNumber?: number;
  totalInstallments?: number;
  onTogglePaid: (id: string) => void;
  onDelete: () => void;
};

function TransactionItem({
  id,
  title,
  description,
  amount,
  category,
  status,
  installmentNumber,
  totalInstallments,
  onTogglePaid,
  onDelete,
}: TransactionItemProps) {
  const isIncome = category === "income";
  const isPaid = status === "paid";
  const hasInstallments =
    typeof installmentNumber === "number" &&
    typeof totalInstallments === "number" &&
    totalInstallments > 1;
  const displayTitle = title || description || "Sem título";

  return (
    <div
      className={`transaction-item ${isPaid ? "transaction-item--paid" : ""}`}
    >
      <div className="transaction-item__info">
        <p
          className={`transaction-item__title ${
            isPaid ? "transaction-item__title--struck" : ""
          }`}
        >
          {displayTitle}
        </p>
        {title && description && (
          <p className="transaction-item__description">{description}</p>
        )}
        {hasInstallments && (
          <p className="transaction-item__meta">
            Parcela {installmentNumber}/{totalInstallments}
          </p>
        )}
      </div>

      <div className="transaction-item__right">
        <div className="transaction-item__actions">
          <Link
            to={`/transactions/${id}/edit`}
            className="transaction-item__action transaction-item__action--edit"
          >
            Editar
          </Link>
          <button
            type="button"
            className="transaction-item__action transaction-item__action--delete"
            onClick={onDelete}
          >
            Excluir
          </button>
        </div>
        {!isIncome && (
          <label className="transaction-item__toggle">
            <input
              type="checkbox"
              checked={isPaid}
              onChange={() => onTogglePaid(id)}
            />
            {isPaid ? "Pago" : "Pendente"}
          </label>
        )}
        <span
          className={`transaction-item__amount ${
            isIncome ? "transaction-item__amount--income" : "transaction-item__amount--expense"
          }`}
        >
          {isIncome ? "+ " : "- "}R$ {amount.toFixed(2)}
        </span>
      </div>
    </div>
  );
}

export default TransactionItem;
