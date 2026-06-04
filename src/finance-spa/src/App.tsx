import { Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar/Navbar";
import "./App.css";
import Dashboard from "./pages/dashboard/Dashboard";
import Transactions from "./pages/Transactions/Transactions";
import NewTransaction from "./pages/NewTransaction/NewTransaction";
import EditTransaction from "./pages/EditTransaction/EditTransaction";
import { useTransactions } from "./hooks/useTransactions";

function App() {
  const {
    transactions,
    loading,
    error,
    saving,
    loadTransactions,
    addTransaction,
    updateTransaction,
    deleteTransaction,
    togglePaid,
  } = useTransactions();

  return (
    <>
      <Navbar />
      <Routes>
        <Route
          path="/"
          element={
            <Dashboard
              transactions={transactions}
              loading={loading}
              error={error}
              onRetry={loadTransactions}
            />
          }
        />
        <Route
          path="/transactions"
          element={
            <Transactions
              transactions={transactions}
              loading={loading}
              error={error}
              onRetry={loadTransactions}
              onTogglePaid={togglePaid}
              onDeleteTransaction={deleteTransaction}
            />
          }
        />
        <Route
          path="/transactions/:id/edit"
          element={
            <EditTransaction
              transactions={transactions}
              onUpdateTransaction={updateTransaction}
              saving={saving}
              error={error}
            />
          }
        />
        <Route
          path="/new"
          element={
            <NewTransaction
              onAddTransaction={addTransaction}
              saving={saving}
              error={error}
            />
          }
        />
      </Routes>
    </>
  );
}

export default App;
