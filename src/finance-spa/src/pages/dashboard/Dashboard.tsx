import { type Transaction } from "../../types/Transaction";
import Summary from "../../components/Summary/Summary";
import PageStatus from "../../components/PageStatus/PageStatus";

type DashboardProps = {
  transactions: Transaction[];
  loading: boolean;
  error: string | null;
  onRetry: () => void;
};

function getCurrentMonth(): number {
  return new Date().getMonth() + 1;
}

function Dashboard({ transactions, loading, error, onRetry }: DashboardProps) {
  const status = <PageStatus loading={loading} error={error} onRetry={onRetry} />;

  if (loading || error) {
    return (
      <div className="page page--dashboard">
        <h1 className="page__title">Dashboard</h1>
        {status}
      </div>
    );
  }

  const currentMonth = getCurrentMonth();
  const monthTransactions = transactions.filter(
    (transaction) => transaction.referenceMonth === currentMonth
  );

  const totalIncome = monthTransactions.reduce((acc, transaction) => {
    return transaction.category === "income"
      ? acc + transaction.amount
      : acc;
  }, 0);

  const totalExpense = monthTransactions.reduce((acc, transaction) => {
    return transaction.category === "expense"
      ? acc + transaction.amount
      : acc;
  }, 0);

  const balance = totalIncome - totalExpense;

  const pendingExpenses = monthTransactions.filter(
    (transaction) =>
      transaction.category === "expense" && transaction.status === "pending"
  );

  const totalPendingExpenses = pendingExpenses.reduce((acc, transaction) => {
    return acc + transaction.amount;
  }, 0);

  return (
    <div className="page page--dashboard">
      <h1 className="page__title">Dashboard</h1>

      <Summary
        income={totalIncome}
        expense={totalExpense}
        balance={balance}
      />

      <div
        style={{
          marginTop: "1rem",
          background: "#fff",
          borderRadius: "12px",
          boxShadow: "0 2px 12px rgba(0, 0, 0, 0.08)",
          padding: "1.25rem 1.5rem",
        }}
      >
        <h3 style={{ margin: 0, marginBottom: "0.75rem", fontSize: "1.0625rem" }}>
          Despesas pendentes do mês
        </h3>
        <p style={{ margin: 0, marginBottom: "0.5rem", fontSize: "0.9rem", color: "#64748b" }}>
          {pendingExpenses.length === 0
            ? "Nenhuma despesa pendente para este mês."
            : `${pendingExpenses.length} despesa(s) pendente(s) somando R$ ${totalPendingExpenses.toFixed(2)}`}
        </p>
        {pendingExpenses.length > 0 && (
          <ul style={{ margin: "0.75rem 0 0", paddingLeft: "1.1rem", fontSize: "0.9rem", color: "#1f2933" }}>
            {pendingExpenses.slice(0, 5).map((t) => (
              <li key={t.id}>
                {t.title || t.description || "Sem título"} — R$ {t.amount.toFixed(2)}
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}

export default Dashboard;
