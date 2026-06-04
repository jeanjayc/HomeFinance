import type {
  FinancaCreateRequest,
  FinancaDTO,
  FinancaUpdateRequest,
} from "../api/types";
import type { Transaction, TransactionStatus } from "../types/Transaction";

function resolveDueDate(referenceMonth: number, dueDay?: number): string {
  const now = new Date();
  let year = now.getFullYear();
  const month = referenceMonth;

  if (month < now.getMonth() + 1) {
    year += 1;
  }

  const day = dueDay ?? 1;
  const daysInMonth = new Date(year, month, 0).getDate();
  const safeDay = Math.min(day, daysInMonth);
  const date = new Date(year, month - 1, safeDay);
  return date.toISOString();
}

export function toTransaction(dto: FinancaDTO): Transaction {
  const status: TransactionStatus = dto.pago ? "paid" : "pending";
  const recurrenceType =
    dto.tipoRecorrencia === "fixed" || dto.tipoRecorrencia === "installment"
      ? dto.tipoRecorrencia
      : "single";

  return {
    id: dto.idFinanca,
    title: dto.titulo ?? "",
    description: dto.descricaoFinanca ?? "",
    amount: dto.valor,
    category: dto.categoria === "income" ? "income" : "expense",
    referenceMonth: dto.mesReferencia,
    status,
    recurrenceType,
    totalInstallments: dto.qtdParcelas ?? undefined,
    installmentNumber: dto.numeroParcela ?? undefined,
    dueDay: dto.diaVencimento ?? undefined,
    templateId: dto.templateId ?? undefined,
  };
}

export function toCreateRequest(transaction: Transaction): FinancaCreateRequest {
  return {
    titulo: transaction.title || null,
    descricaoFinanca: transaction.description || transaction.title || "Lançamento",
    valor: transaction.amount,
    dataVencimento: resolveDueDate(transaction.referenceMonth, transaction.dueDay),
    mesReferencia: transaction.referenceMonth,
    categoria: transaction.category,
    tipoRecorrencia: transaction.recurrenceType,
    qtdParcelas: transaction.totalInstallments ?? null,
    numeroParcela: transaction.installmentNumber ?? null,
    diaVencimento: transaction.dueDay ?? null,
    templateId: transaction.templateId ?? null,
    pago: transaction.status === "paid",
  };
}

export function toUpdateRequest(transaction: Transaction): FinancaUpdateRequest {
  return {
    idFinanca: transaction.id,
    titulo: transaction.title || null,
    descricaoFinanca: transaction.description || transaction.title || "Lançamento",
    valor: transaction.amount,
    dataVencimento: resolveDueDate(transaction.referenceMonth, transaction.dueDay),
    mesReferencia: transaction.referenceMonth,
    categoria: transaction.category,
    tipoRecorrencia: transaction.recurrenceType,
    qtdParcelas: transaction.totalInstallments ?? null,
    numeroParcela: transaction.installmentNumber ?? null,
    diaVencimento: transaction.dueDay ?? null,
    templateId: transaction.templateId ?? null,
    pago: transaction.status === "paid",
  };
}
