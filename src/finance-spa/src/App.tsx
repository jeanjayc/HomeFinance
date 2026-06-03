
import { Routes, Route } from 'react-router-dom';


import Navbar from './components/Navbar/Navbar';

import './App.css';
import Dashboard from './pages/dashboard/Dashboard';
import Transactions from './pages/Transactions/Transactions';
import NewTransaction from './pages/NewTransaction/NewTransaction';
import { useTransactions } from "./hooks/useTransactions";

function App() {
  const { transactions, addTransaction, togglePaid } = useTransactions();

  return (
    <>
      <Navbar />
      <Routes>
        <Route
          path="/"
          element={<Dashboard transactions={transactions} />}
        />
        <Route
          path="/transactions"
          element={<Transactions transactions={transactions} onTogglePaid={togglePaid} />}
        />
        <Route
          path="/new"
          element={
            <NewTransaction onAddTransaction={addTransaction} /> 
          }
        />
      </Routes>
    </>
  )
}

export default App
