import { useState } from "react";

import { type Transaction } from "../../types/Transaction";

import Summary from "../../components/Summary/Summary";

import PageStatus from "../../components/PageStatus/PageStatus";

import { getCurrentMonth, MONTH_LABELS, shiftMonth } from "../../utils/months";

import "./Dashboard.css";



type DashboardProps = {

  transactions: Transaction[];

  loading: boolean;

  error: string | null;

  onRetry: () => void;

};



function Dashboard({ transactions, loading, error, onRetry }: DashboardProps) {

  const [selectedMonth, setSelectedMonth] = useState(getCurrentMonth);

  const status = <PageStatus loading={loading} error={error} onRetry={onRetry} />;

  const currentMonth = getCurrentMonth();

  const isCurrentMonth = selectedMonth === currentMonth;

  const monthLabel = MONTH_LABELS[selectedMonth - 1];



  if (loading || error) {

    return (

      <div className="page page--dashboard">

        <h1 className="page__title">Dashboard</h1>

        {status}

      </div>

    );

  }



  const monthTransactions = transactions.filter(

    (transaction) => transaction.referenceMonth === selectedMonth

  );



  const totalIncome = monthTransactions.reduce((acc, transaction) => {

    return transaction.category === "income"

      ? acc + transaction.amount

      : acc;

  }, 0);



  const totalPaidExpenses = monthTransactions.reduce((acc, transaction) => {

    return transaction.category === "expense" && transaction.status === "paid"

      ? acc + transaction.amount

      : acc;

  }, 0);



  const balance = totalIncome - totalPaidExpenses;



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



      <div className="dashboard__month-nav">

        <button

          type="button"

          className="dashboard__month-btn"

          onClick={() => setSelectedMonth((m) => shiftMonth(m, -1))}

          aria-label="Mês anterior"

        >

          ◀

        </button>

        <span className="dashboard__month-label">

          {monthLabel}

          {isCurrentMonth && (

            <span className="dashboard__month-badge">Mês atual</span>

          )}

        </span>

        <button

          type="button"

          className="dashboard__month-btn"

          onClick={() => setSelectedMonth((m) => shiftMonth(m, 1))}

          aria-label="Próximo mês"

        >

          ▶

        </button>

        {!isCurrentMonth && (

          <button

            type="button"

            className="dashboard__current-month-btn"

            onClick={() => setSelectedMonth(currentMonth)}

          >

            Mês atual

          </button>

        )}

      </div>



      <Summary

        income={totalIncome}

        expense={totalPaidExpenses}

        balance={balance}

      />



      <div className="dashboard__pending">

        <h3 className="dashboard__pending-title">

          Despesas pendentes de {monthLabel}

        </h3>

        <p className="dashboard__pending-text">

          {pendingExpenses.length === 0

            ? `Nenhuma despesa pendente para ${monthLabel}.`

            : `${pendingExpenses.length} despesa(s) pendente(s) somando R$ ${totalPendingExpenses.toFixed(2)}`}

        </p>

        {pendingExpenses.length > 0 && (

          <ul className="dashboard__pending-list">

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

