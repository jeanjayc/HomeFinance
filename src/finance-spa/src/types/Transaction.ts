export type RecurrenceType = "single" | "fixed" | "installment";

export type TransactionStatus = "pending" | "paid";

export interface Transaction {
  id: string;
  title: string;
  description: string;
  amount: number;
  category: "income" | "expense";
  /** Mês de referência (1–12, alinhado ao backend MesReferencia). */
  referenceMonth: number;
  status: TransactionStatus;
  recurrenceType: RecurrenceType;
  totalInstallments?: number;
  installmentNumber?: number;
  dueDay?: number;
  templateId?: string;
}

