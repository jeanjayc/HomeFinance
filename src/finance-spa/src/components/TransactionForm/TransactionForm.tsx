import { useState } from "react";
import { type Transaction, type RecurrenceType } from "../../types/Transaction";
import "./TransactionForm.css";

type TransactionFormProps = {
    onAddTransaction: (transaction: Transaction) => void;
};

function getCurrentMonth(): string {
  const now = new Date();
  const month = String(now.getMonth() + 1).padStart(2, "0");
  return `${now.getFullYear()}-${month}`;
}

function TransactionForm({ onAddTransaction }: TransactionFormProps) {
    const [description, setDescription] = useState("");
    const [amount, setAmount] = useState<number>(0);
    const [category, setCategory] = useState<"income" | "expense">("expense");
    const [title, setTitle] = useState("");
    const [recurrenceType, setRecurrenceType] = useState<RecurrenceType>("single");
    const [totalInstallments, setTotalInstallments] = useState<number>(1);
    const [referenceMonth, setReferenceMonth] = useState<string>(getCurrentMonth());
    const [dueDay, setDueDay] = useState<number | undefined>(undefined);

    function handleSubmit(event: React.FormEvent<HTMLFormElement>){
        event.preventDefault();

        const month = referenceMonth || getCurrentMonth();

        const newTransaction: Transaction = {
            id: Date.now().toString(),
            title,
            description,
            amount,
            category,
            referenceMonth: month,
            status: "pending",
            recurrenceType,
            totalInstallments: recurrenceType === "installment" ? totalInstallments || 1 : undefined,
            installmentNumber: recurrenceType === "installment" ? 1 : undefined,
            dueDay,
        };

        onAddTransaction(newTransaction);

        setTitle("");
        setDescription("");
        setAmount(0);
        setCategory("expense");
        setRecurrenceType("single");
        setTotalInstallments(1);
        setReferenceMonth(getCurrentMonth());
        setDueDay(undefined);
    }

    return (
    <form className="transaction-form" onSubmit={handleSubmit}>
      <h3 className="transaction-form__title">Novo lançamento</h3>

      <div className="transaction-form__field">
        <label className="transaction-form__label" htmlFor="transaction-title">Título</label>
        <input
          id="transaction-title"
          type="text"
          className="transaction-form__input"
          placeholder="Ex.: Aluguel, Supermercado"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
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
        />
      </div>

      <div className="transaction-form__field">
        <label className="transaction-form__label" htmlFor="transaction-category">Tipo</label>
        <select
          id="transaction-category"
          className="transaction-form__select"
          value={category}
          onChange={(e) => setCategory(e.target.value as "income" | "expense")}
        >
          <option value="income">Receita</option>
          <option value="expense">Despesa</option>
        </select>
      </div>

      <div className="transaction-form__field">
        <label className="transaction-form__label" htmlFor="transaction-recurrence">Recorrência</label>
        <select
          id="transaction-recurrence"
          className="transaction-form__select"
          value={recurrenceType}
          onChange={(e) => setRecurrenceType(e.target.value as RecurrenceType)}
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
          />
        </div>
      )}

      <div className="transaction-form__row">
        <div className="transaction-form__field">
          <label className="transaction-form__label" htmlFor="transaction-month">Mês de referência</label>
          <input
            id="transaction-month"
            type="month"
            className="transaction-form__input"
            value={referenceMonth}
            onChange={(e) => setReferenceMonth(e.target.value)}
          />
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
          />
        </div>
      </div>

      <button type="submit" className="transaction-form__submit">Adicionar lançamento</button>
    </form>
  );
}

export default TransactionForm;