﻿using HomeFinance.Domain.Models;
using HomeFinance.Infra.DTOs.Response.Financas;

namespace HomeFinance.Application.Interfaces
{
    public interface IFinancesService
    {
        Task AdicionarNovasDividas(Finances finance);

        Task<Finances> AtualizarDadosFinancas(Guid id,Finances finances);

        Task<Finances> BuscarFinancaPorId(Guid id);

        Task<Finances> BuscarFinancaPorNome(string name);

        Task<List<Finances>> BuscarTodasFinancas();

        Task<IEnumerable<FinancaDTO>> BuscarTodasFinancasNaoPagas();

        Task<string> BuscarVencimentoProximo();

        Task<decimal> CalcularGastos(decimal renda);

        Task<Finances> DeletarFinancas(Guid id);

        Task<decimal> AlterarValorPago(Guid id);

        Task<decimal> SomarTotalFinancas();
    }
}