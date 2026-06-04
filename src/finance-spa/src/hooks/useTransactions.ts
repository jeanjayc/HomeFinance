import { useCallback, useEffect, useState } from "react";
import {
  alternarPago,
  atualizarFinanca,
  buscarTodasFinancas,
  criarFinanca,
  deletarFinanca,
} from "../api/financasApi";
import { ApiError } from "../api/client";
import { toCreateRequest, toTransaction, toUpdateRequest } from "../mappers/financaMapper";
import { type Transaction } from "../types/Transaction";

export function useTransactions() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [saving, setSaving] = useState(false);

  const loadTransactions = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await buscarTodasFinancas();
      setTransactions(data.map(toTransaction));
    } catch (err) {
      const message =
        err instanceof ApiError
          ? `Erro ao carregar lançamentos (${err.status})`
          : "Erro ao carregar lançamentos";
      setError(message);
      setTransactions([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void loadTransactions();
  }, [loadTransactions]);

  async function addTransaction(transaction: Transaction) {
    setSaving(true);
    setError(null);
    try {
      const created = await criarFinanca(toCreateRequest(transaction));
      if (
        transaction.recurrenceType === "installment" &&
        (transaction.totalInstallments ?? 0) > 1
      ) {
        await loadTransactions();
      } else {
        setTransactions((prev) => [...prev, toTransaction(created)]);
      }
    } catch (err) {
      const message =
        err instanceof ApiError
          ? `Erro ao salvar lançamento (${err.status})`
          : "Erro ao salvar lançamento";
      setError(message);
      throw err;
    } finally {
      setSaving(false);
    }
  }

  async function updateTransaction(transaction: Transaction) {
    setSaving(true);
    setError(null);
    try {
      const updated = await atualizarFinanca(toUpdateRequest(transaction));
      setTransactions((prev) =>
        prev.map((t) => (t.id === transaction.id ? toTransaction(updated) : t))
      );
    } catch (err) {
      const message =
        err instanceof ApiError
          ? `Erro ao atualizar lançamento (${err.status})`
          : "Erro ao atualizar lançamento";
      setError(message);
      throw err;
    } finally {
      setSaving(false);
    }
  }

  async function deleteTransaction(id: string) {
    setError(null);
    const previous = transactions;
    setTransactions((prev) => prev.filter((t) => t.id !== id));

    try {
      await deletarFinanca(id);
    } catch (err) {
      setTransactions(previous);
      const message =
        err instanceof ApiError
          ? `Erro ao excluir lançamento (${err.status})`
          : "Erro ao excluir lançamento";
      setError(message);
      throw err;
    }
  }

  async function togglePaid(id: string) {
    setError(null);
    const previous = transactions;
    setTransactions((prev) =>
      prev.map((t) =>
        t.id === id
          ? { ...t, status: t.status === "paid" ? "pending" : "paid" }
          : t
      )
    );

    try {
      const updated = await alternarPago(id);
      setTransactions((prev) =>
        prev.map((t) => (t.id === id ? toTransaction(updated) : t))
      );
    } catch (err) {
      setTransactions(previous);
      const message =
        err instanceof ApiError
          ? `Erro ao atualizar status (${err.status})`
          : "Erro ao atualizar status";
      setError(message);
    }
  }

  return {
    transactions,
    loading,
    error,
    saving,
    loadTransactions,
    addTransaction,
    updateTransaction,
    deleteTransaction,
    togglePaid,
  };
}
