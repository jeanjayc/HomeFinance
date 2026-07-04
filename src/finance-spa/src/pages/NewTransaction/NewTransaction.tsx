import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { type Transaction } from "../../types/Transaction";
import TransactionForm from "../../components/TransactionForm/TransactionForm";
import PageStatus from "../../components/PageStatus/PageStatus";

type NewTransactionProps = {
  onAddTransaction: (transaction: Transaction) => Promise<void>;
  saving: boolean;
  error: string | null;
};

function NewTransaction({ onAddTransaction, saving, error }: NewTransactionProps) {
  const navigate = useNavigate();
  const [submitError, setSubmitError] = useState<string | null>(null);

  async function handleAdd(transaction: Transaction) {
    setSubmitError(null);
    try {
      await onAddTransaction(transaction);
      navigate("/transactions");
    } catch {
      setSubmitError("Não foi possível salvar o lançamento.");
    }
  }

  return (
    <div className="page page--new-transaction">
      <PageStatus error={error ?? submitError} />
      <TransactionForm onSubmit={handleAdd} saving={saving} />
    </div>
  );
}

export default NewTransaction;
