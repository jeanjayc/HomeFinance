import type { Transaction } from "../types/Transaction";

export type TransactionListItem =
  | { kind: "single"; transaction: Transaction }
  | {
      kind: "installmentGroup";
      templateId: string;
      installments: Transaction[];
    };

const monthYearFormatter = new Intl.DateTimeFormat("pt-BR", {
  month: "long",
  year: "numeric",
});

function inferDueDateIso(transaction: Transaction): string {
  const now = new Date();
  let year = now.getFullYear();
  const month = transaction.referenceMonth;

  if (month < now.getMonth() + 1) {
    year += 1;
  }

  const day = transaction.dueDay ?? 1;
  const daysInMonth = new Date(year, month, 0).getDate();
  const safeDay = Math.min(day, daysInMonth);
  return new Date(year, month - 1, safeDay).toISOString();
}

function resolveDueDate(transaction: Transaction): Date {
  const iso = transaction.dueDate ?? inferDueDateIso(transaction);
  return new Date(iso);
}

export function isInstallmentGroupMember(transaction: Transaction): boolean {
  return (
    transaction.recurrenceType === "installment" &&
    (transaction.totalInstallments ?? 0) > 1 &&
    Boolean(transaction.templateId)
  );
}

function compareInstallments(a: Transaction, b: Transaction): number {
  const dateA = resolveDueDate(a).getTime();
  const dateB = resolveDueDate(b).getTime();
  if (dateA !== dateB) return dateA - dateB;

  const numA = a.installmentNumber ?? 0;
  const numB = b.installmentNumber ?? 0;
  return numA - numB;
}

export function formatDueMonthYear(transaction: Transaction): string {
  const formatted = monthYearFormatter.format(resolveDueDate(transaction));
  const [month, year] = formatted.split(" de ");
  if (!month || !year) return formatted;
  const capitalizedMonth =
    month.charAt(0).toUpperCase() + month.slice(1);
  return `${capitalizedMonth}/${year}`;
}

export type InstallmentGroupSummary = {
  totalCount: number;
  paidCount: number;
  pendingCount: number;
  paidAmount: number;
  pendingAmount: number;
  summaryText: string;
};

function formatCurrency(amount: number): string {
  return `R$ ${amount.toFixed(2)}`;
}

export function summarizeInstallmentGroup(
  installments: Transaction[]
): InstallmentGroupSummary {
  let paidCount = 0;
  let pendingCount = 0;
  let paidAmount = 0;
  let pendingAmount = 0;

  for (const installment of installments) {
    if (installment.status === "paid") {
      paidCount += 1;
      paidAmount += installment.amount;
    } else {
      pendingCount += 1;
      pendingAmount += installment.amount;
    }
  }

  const totalCount = installments.length;
  const countPart = `${totalCount} parcela${totalCount === 1 ? "" : "s"}`;
  const statusPart =
    paidCount > 0 || pendingCount > 0
      ? `${paidCount} paga${paidCount === 1 ? "" : "s"} · ${pendingCount} pendente${pendingCount === 1 ? "" : "s"}`
      : "";
  const amountPart =
    paidAmount > 0 || pendingAmount > 0
      ? `${formatCurrency(paidAmount)} pagos · ${formatCurrency(pendingAmount)} pendentes`
      : "";

  const summaryText = [countPart, statusPart, amountPart]
    .filter(Boolean)
    .join(" · ");

  return {
    totalCount,
    paidCount,
    pendingCount,
    paidAmount,
    pendingAmount,
    summaryText,
  };
}

export function buildTransactionListItems(
  transactions: Transaction[]
): TransactionListItem[] {
  const groupMap = new Map<string, Transaction[]>();

  for (const transaction of transactions) {
    if (!isInstallmentGroupMember(transaction) || !transaction.templateId) {
      continue;
    }

    const templateId = transaction.templateId;
    const existing = groupMap.get(templateId);
    if (existing) {
      existing.push(transaction);
    } else {
      groupMap.set(templateId, [transaction]);
    }
  }

  for (const installments of groupMap.values()) {
    installments.sort(compareInstallments);
  }

  const emittedGroups = new Set<string>();
  const items: TransactionListItem[] = [];

  transactions.forEach((transaction) => {
    if (isInstallmentGroupMember(transaction) && transaction.templateId) {
      const templateId = transaction.templateId;
      if (emittedGroups.has(templateId)) return;

      emittedGroups.add(templateId);
      items.push({
        kind: "installmentGroup",
        templateId,
        installments: groupMap.get(templateId) ?? [transaction],
      });
      return;
    }

    items.push({ kind: "single", transaction });
  });

  return items;
}

export function getInstallmentGroupTitle(installments: Transaction[]): string {
  const first = installments[0];
  if (!first) return "Sem título";
  return first.title || first.description || "Sem título";
}
