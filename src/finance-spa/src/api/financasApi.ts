import { apiFetch } from "./client";
import type { FinancaCreateRequest, FinancaDTO, FinancaUpdateRequest } from "./types";

const base = "/api/Financas";

export function buscarTodasFinancas(): Promise<FinancaDTO[]> {
  return apiFetch<FinancaDTO[]>(`${base}/BuscarTodasFinancas`);
}

export function buscarFinancaPorId(id: string): Promise<FinancaDTO> {
  return apiFetch<FinancaDTO>(`${base}/BuscarFinancaPorId?id=${id}`);
}

export function criarFinanca(request: FinancaCreateRequest): Promise<FinancaDTO> {
  return apiFetch<FinancaDTO>(`${base}/CriarFinanca`, {
    method: "POST",
    body: JSON.stringify(request),
  });
}

export function atualizarFinanca(request: FinancaUpdateRequest): Promise<FinancaDTO> {
  return apiFetch<FinancaDTO>(`${base}/AtualizarFinanca`, {
    method: "PUT",
    body: JSON.stringify(request),
  });
}

export function alternarPago(id: string): Promise<FinancaDTO> {
  return apiFetch<FinancaDTO>(`${base}/AlternarPago?id=${id}`, {
    method: "POST",
  });
}

export function deletarFinanca(id: string): Promise<void> {
  return apiFetch<void>(`${base}/DeletarFinanca?id=${id}`, {
    method: "DELETE",
  });
}
