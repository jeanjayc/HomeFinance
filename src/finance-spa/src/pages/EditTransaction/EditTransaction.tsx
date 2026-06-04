import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { buscarFinancaPorId } from "../../api/financasApi";
import { ApiError } from "../../api/client";
import TransactionForm from "../../components/TransactionForm/TransactionForm";
import PageStatus from "../../components/PageStatus/PageStatus";
import { toTransaction } from "../../mappers/financaMapper";
import { type Transaction } from "../../types/Transaction";

type EditTransactionProps = {
  transactions: Transaction[];
  onUpdateTransaction: (transaction: Transaction) => Promise<void>;
  saving: boolean;
  error: string | null;
};

function EditTransaction({
  transactions,
  onUpdateTransaction,
  saving,
  error,
}: EditTransactionProps) {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [transaction, setTransaction] = useState<Transaction | null>(
    () => transactions.find((t) => t.id === id) ?? null
  );
  const [loading, setLoading] = useState(!transaction);
  const [loadError, setLoadError] = useState<string | null>(null);
  const [submitError, setSubmitError] = useState<string | null>(null);

  useEffect(() => {
    if (transaction || !id) return;

    let cancelled = false;

    async function load() {
      setLoading(true);
      setLoadError(null);
      try {
        const dto = await buscarFinancaPorId(id!);
        if (!cancelled) {
          setTransaction(toTransaction(dto));
        }
      } catch (err) {
        if (!cancelled) {
          const message =
            err instanceof ApiError
              ? `Lançamento não encontrado (${err.status})`
              : "Lançamento não encontrado";
          setLoadError(message);
        }
      } finally {
        if (!cancelled) {
          setLoading(false);
        }
      }
    }

    void load();

    return () => {
      cancelled = true;
    };
  }, [id, transaction]);

  async function handleUpdate(updated: Transaction) {
    setSubmitError(null);
    try {
      await onUpdateTransaction(updated);
      navigate("/transactions");
    } catch {
      setSubmitError("Não foi possível salvar as alterações.");
    }
  }

  if (!id) {
    return (
      <div className="page page--edit-transaction">
        <h1 className="page__title">Editar lançamento</h1>
        <p>Identificador inválido.</p>
        <Link to="/transactions">Voltar aos lançamentos</Link>
      </div>
    );
  }

  return (
    <div className="page page--edit-transaction">
      <h1 className="page__title">Editar lançamento</h1>
      <PageStatus
        loading={loading}
        error={loadError ?? error ?? submitError}
      />
      {!loading && !loadError && transaction && (
        <TransactionForm
          initialTransaction={transaction}
          onSubmit={handleUpdate}
          saving={saving}
        />
      )}
      {!loading && loadError && (
        <Link to="/transactions">Voltar aos lançamentos</Link>
      )}
    </div>
  );
}

export default EditTransaction;
