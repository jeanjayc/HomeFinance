export const MONTH_LABELS = [
  "Janeiro",
  "Fevereiro",
  "Março",
  "Abril",
  "Maio",
  "Junho",
  "Julho",
  "Agosto",
  "Setembro",
  "Outubro",
  "Novembro",
  "Dezembro",
];

export function getCurrentMonth(): number {
  return new Date().getMonth() + 1;
}

export function shiftMonth(month: number, delta: number): number {
  return ((month - 1 + delta + 12) % 12) + 1;
}
