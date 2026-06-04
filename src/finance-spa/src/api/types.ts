export interface FinancaDTO {
  idFinanca: string;
  titulo?: string | null;
  descricaoFinanca?: string | null;
  dataVencimento: string;
  valor: number;
  pago: boolean;
  qtdParcelas?: number | null;
  categoria: string;
  mesReferencia: number;
  tipoRecorrencia: string;
  numeroParcela?: number | null;
  diaVencimento?: number | null;
  templateId?: string | null;
}

export interface FinancaCreateRequest {
  titulo?: string | null;
  descricaoFinanca: string;
  valor: number;
  dataVencimento?: string;
  mesReferencia: number;
  categoria: string;
  tipoRecorrencia: string;
  qtdParcelas?: number | null;
  numeroParcela?: number | null;
  diaVencimento?: number | null;
  templateId?: string | null;
  pago: boolean;
}

export interface FinancaUpdateRequest {
  idFinanca: string;
  titulo?: string | null;
  descricaoFinanca: string;
  valor: number;
  dataVencimento?: string;
  mesReferencia: number;
  categoria: string;
  tipoRecorrencia: string;
  qtdParcelas?: number | null;
  numeroParcela?: number | null;
  diaVencimento?: number | null;
  templateId?: string | null;
  pago: boolean;
}
