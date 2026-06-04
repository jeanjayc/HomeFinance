import { useState } from "react";
import TransactionItem from "../TransactionItem/TransactionItem";
import { type Transaction } from "../../types/Transaction";
import {
  formatDueMonthYear,
  getInstallmentGroupTitle,
  summarizeInstallmentGroup,
} from "../../utils/installmentGrouping";
import "./InstallmentGroup.css";

type InstallmentGroupProps = {
  installments: Transaction[];
  onTogglePaid: (id: string) => void;
  onDelete: (id: string, label: string) => void;
};

function InstallmentGroup({
  installments,
  onTogglePaid,
  onDelete,
}: InstallmentGroupProps) {
  const [expanded, setExpanded] = useState(false);
  const title = getInstallmentGroupTitle(installments);
  const { totalCount, summaryText } = summarizeInstallmentGroup(installments);

  return (
    <div className="installment-group">
      <button
        type="button"
        className="installment-group__header"
        onClick={() => setExpanded((prev) => !prev)}
        aria-expanded={expanded}
      >
        <span className="installment-group__icon" aria-hidden>
          📁
        </span>
        <span
          className={`installment-group__chevron ${
            expanded ? "installment-group__chevron--expanded" : ""
          }`}
          aria-hidden
        >
          ▶
        </span>
        <div className="installment-group__content">
          <p className="installment-group__title">
            {title} ({totalCount})
          </p>
          <p className="installment-group__summary">{summaryText}</p>
        </div>
      </button>

      {expanded && (
        <div className="installment-group__children">
          {installments.map((installment) => (
            <TransactionItem
              key={installment.id}
              id={installment.id}
              title={installment.title}
              description={installment.description}
              amount={installment.amount}
              category={installment.category}
              status={installment.status}
              installmentNumber={installment.installmentNumber}
              totalInstallments={installment.totalInstallments}
              displayLabel={formatDueMonthYear(installment)}
              hideInstallmentMeta
              variant="installment-child"
              onTogglePaid={onTogglePaid}
              onDelete={() =>
                onDelete(
                  installment.id,
                  formatDueMonthYear(installment)
                )
              }
            />
          ))}
        </div>
      )}
    </div>
  );
}

export default InstallmentGroup;
