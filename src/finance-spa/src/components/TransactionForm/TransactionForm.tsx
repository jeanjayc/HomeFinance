import { useState } from "react";
import { type Transaction, type RecurrenceType } from "../../types/Transaction";
import "./TransactionForm.css";

const MONTH_LABELS = [
  "Janeiro",
  "Fevereiro",
  "Março",
  "Abril",
  "Maio",
  "Junho",
  "Julho",
  "Agosto",
  "Setembro",
  "Outubro",
  "Novembro",
  "Dezembro",
];

type TransactionFormProps = {
  onSubmit: (transaction: Transaction) => void | Promise<void>;
  saving?: boolean;
  initialTransaction?: Transaction;
  submitLabel?: string;
  title?: string;
};

function getCurrentMonth(): number {
  return new Date().getMonth() + 1;
}

function TransactionForm({
  onSubmit,
  saving = false,
  initialTransaction,
  submitLabel,
  title: formTitle,
}: TransactionFormProps) {
  const isEdit = Boolean(initialTransaction);

  const [description, setDescription] = useState(initialTransaction?.description ?? "");
  const [amount, setAmount] = useState<number>(initialTransaction?.amount ?? 0);
  const [category, setCategory] = useState<"income" | "expense">(
    initialTransaction?.category ?? "expense"
  );
  const [transactionTitle, setTransactionTitle] = useState(initialTransaction?.title ?? "");
  const [recurrenceType, setRecurrenceType] = useState<RecurrenceType>(
    initialTransaction?.recurrenceType ?? "single"
  );
  const [totalInstallments, setTotalInstallments] = useState<number>(
    initialTransaction?.totalInstallments ?? 1
  );
  const [referenceMonth, setReferenceMonth] = useState<number>(
    initialTransaction?.referenceMonth ?? getCurrentMonth()
  );
  const [dueDay, setDueDay] = useState<number | undefined>(initialTransaction?.dueDay);
  const [status, setStatus] = useState<"pending" | "paid">(
    initialTransaction?.status ?? "pending"
  );

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const month = referenceMonth || getCurrentMonth();

    const transaction: Transaction = {
      id: initialTransaction?.id ?? crypto.randomUUID(),
      title: transactionTitle,
      description,
      amount,
      category,
      referenceMonth: month,
      status: isEdit ? status : "pending",
      recurrenceType: initialTransaction?.recurrenceType ?? recurrenceType,
      totalInstallments:
        (initialTransaction?.recurrenceType ?? recurrenceType) === "installment"
          ? (initialTransaction?.totalInstallments ?? totalInstallments) || 1
          : undefined,
      installmentNumber: initialTransaction?.installmentNumber,
      dueDay,
      templateId: initialTransaction?.templateId,
    };

    await onSubmit(transaction);

    if (!isEdit) {
      setTransactionTitle("");
      setDescription("");
      setAmount(0);
      setCategory("expense");
      setRecurrenceType("single");
      setTotalInstallments(1);
      setReferenceMonth(getCurrentMonth());
      setDueDay(undefined);
      setStatus("pending");
    }
  }

  return (
    <form className="transaction-form" onSubmit={handleSubmit}>
      <h3 className="transaction-form__title">
        {formTitle ?? (isEdit ? "Editar lançamento" : "Novo lançamento")}
      </h3>

      <div className="transaction-form__field">
        <label className="transaction-form__label" htmlFor="transaction-title">Título</label>
        <input
          id="transaction-title"
          type="text"
          className="transaction-form__input"
          placeholder="Ex.: Aluguel, Supermercado"
          value={transactionTitle}
          onChange={(e) => setTransactionTitle(e.target.value)}
          disabled={saving}
        />
      </div>

      <div className="transaction-form__field">
        <label className="transaction-form__label" htmlFor="transaction-description">Descrição</label>
        <input
          id="transaction-description"
          type="text"
          className="transaction-form__input"
          placeholder="Detalhes opcionais"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          disabled={saving}
        />
      </div>

      <div className="transaction-form__field">
        <label className="transaction-form__label" htmlFor="transaction-amount">Valor (R$)</label>
        <input
          id="transaction-amount"
          type="number"
          min={0}
          step={0.01}
          className="transaction-form__input"
          placeholder="0,00"
          value={amount || ""}
          onChange={(e) => setAmount(Number(e.target.value) || 0)}
          disabled={saving}
          required
        />
      </div>

      <div className="transaction-form__field">
        <label className="transaction-form__label" htmlFor="transaction-category">Tipo</label>
        <select
          id="transaction-category"
          className="transaction-form__select"
          value={category}
          onChange={(e) => setCategory(e.target.value as "income" | "expense")}
          disabled={saving}
        >
          <option value="income">Receita</option>
          <option value="expense">Despesa</option>
        </select>
      </div>

      {isEdit && (
        <div className="transaction-form__field">
          <label className="transaction-form__label" htmlFor="transaction-status">Status</label>
          <select
            id="transaction-status"
            className="transaction-form__select"
            value={status}
            onChange={(e) => setStatus(e.target.value as "pending" | "paid")}
            disabled={saving}
          >
            <option value="pending">Pendente</option>
            <option value="paid">Pago</option>
          </select>
        </div>
      )}

      {!isEdit && (
        <>
          <div className="transaction-form__field">
            <label className="transaction-form__label" htmlFor="transaction-recurrence">Recorrência</label>
            <select
              id="transaction-recurrence"
              className="transaction-form__select"
              value={recurrenceType}
              onChange={(e) => setRecurrenceType(e.target.value as RecurrenceType)}
              disabled={saving}
            >
              <option value="single">Lançamento único</option>
              <option value="fixed">Fixo mensal</option>
              <option value="installment">Parcelado</option>
            </select>
          </div>

          {recurrenceType === "installment" && (
            <div className="transaction-form__field">
              <label className="transaction-form__label" htmlFor="transaction-installments">Número de parcelas</label>
              <input
                id="transaction-installments"
                type="number"
                min={1}
                className="transaction-form__input"
                value={totalInstallments}
                onChange={(e) => setTotalInstallments(Number(e.target.value))}
                disabled={saving}
              />
            </div>
          )}
        </>
      )}

      {isEdit && initialTransaction?.installmentNumber && initialTransaction?.totalInstallments && (
        <p className="transaction-form__meta">
          Parcela {initialTransaction.installmentNumber}/{initialTransaction.totalInstallments}
        </p>
      )}

      <div className="transaction-form__row">
        <div className="transaction-form__field">
          <label className="transaction-form__label" htmlFor="transaction-month">Mês de referência</label>
          <select
            id="transaction-month"
            className="transaction-form__select"
            value={referenceMonth}
            onChange={(e) => setReferenceMonth(Number(e.target.value))}
            disabled={saving}
            required
          >
            {MONTH_LABELS.map((label, index) => (
              <option key={label} value={index + 1}>
                {label}
              </option>
            ))}
          </select>
        </div>
        <div className="transaction-form__field">
          <label className="transaction-form__label" htmlFor="transaction-due">
            Dia venc. <span className="transaction-form__optional">(opcional)</span>
          </label>
          <input
            id="transaction-due"
            type="number"
            min={1}
            max={31}
            className="transaction-form__input"
            placeholder="1–31"
            value={dueDay ?? ""}
            onChange={(e) => {
              const value = e.target.value;
              setDueDay(value ? Number(value) : undefined);
            }}
            disabled={saving}
          />
        </div>
      </div>

      <button type="submit" className="transaction-form__submit" disabled={saving}>
        {saving ? "Salvando..." : (submitLabel ?? (isEdit ? "Salvar alterações" : "Adicionar lançamento"))}
      </button>
    </form>
  );
}

export default TransactionForm;
