import { useState, useEffect } from "react";
import { type Transaction } from "../types/Transaction";

function getCurrentMonth(): string {
  const now = new Date();
  const month = String(now.getMonth() + 1).padStart(2, "0");
  return `${now.getFullYear()}-${month}`;
}

function normalizeTransaction(raw: Record<string, unknown>): Transaction {
  const referenceMonth = typeof raw.referenceMonth === "string" ? raw.referenceMonth : getCurrentMonth();
  const status = raw.status === "paid" ? "paid" : "pending";
  const recurrenceType =
    raw.recurrenceType === "fixed" || raw.recurrenceType === "installment" ? raw.recurrenceType : "single";

  return {
    id: typeof raw.id === "string" ? raw.id : Date.now().toString(),
    title: typeof raw.title === "string" ? raw.title : "",
    description: typeof raw.description === "string" ? raw.description : "",
    amount: typeof raw.amount === "number" ? raw.amount : 0,
    category: raw.category === "income" ? "income" : "expense",
    referenceMonth,
    status,
    recurrenceType,
    totalInstallments: typeof raw.totalInstallments === "number" ? raw.totalInstallments : undefined,
    installmentNumber: typeof raw.installmentNumber === "number" ? raw.installmentNumber : undefined,
    dueDay: typeof raw.dueDay === "number" ? raw.dueDay : undefined,
    templateId: typeof raw.templateId === "string" ? raw.templateId : undefined,
  };
}

export function useTransactions() {
    const [transactions, setTransactions] = useState<Transaction[]>(() => {
        const storedTransactions = localStorage.getItem("transactions");

        if (!storedTransactions) {
          return [];
        }

        try {
          const parsed = JSON.parse(storedTransactions);
          if (!Array.isArray(parsed)) {
            return [];
          }
          return parsed.map(normalizeTransaction);
        } catch {
          return [];
        }
    });

    useEffect(() => {
      localStorage.setItem("transactions", JSON.stringify(transactions));
    }, [transactions]);

    function addTransaction(transaction: Transaction) {
      setTransactions((prev) => [...prev, transaction]);
    }

    function deleteTransaction(id: string) {
      setTransactions((prev) => prev.filter((t) => t.id !== id));
    }

    function togglePaid(id: string) {
      setTransactions((prev) =>
        prev.map((t) =>
          t.id === id
            ? {
                ...t,
                status: t.status === "paid" ? "pending" : "paid",
              }
            : t
        )
      );
    }

    return {
        transactions,
        addTransaction,
        deleteTransaction,
        togglePaid,
    };
}