export type RecurrenceType = "single" | "fixed" | "installment";

export type TransactionStatus = "pending" | "paid";

export interface Transaction {
  id: string;
  title: string;
  description: string;
  amount: number;
  category: "income" | "expense";
  /**rem
   * Mês de referência no formato YYYY-MM (ex.: "2026-03").
   */
  referenceMonth: string;
  status: TransactionStatus;
  recurrenceType: RecurrenceType;
  totalInstallments?: number;
  installmentNumber?: number;
  dueDay?: number;
  templateId?: string;
}

