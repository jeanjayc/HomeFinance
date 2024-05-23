using Dapper;
using HomeFinance.Infra.DTOs.Response.Financas;
using HomeFinance.Infra.Interfaces.DAO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace HomeFinance.Infra.DAO
{
    public class FinancasDAO : IFinancaDAO
    {
        private readonly IDbConnection _connection;

        public FinancasDAO(IConfiguration configuration)
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("Connection")); 
        }

        public async Task<FinancaDTO> ObterFinancaPorDescricao(string descricao)
        {
            var consulta = @"SELECT 
                              id_finances as IdFinanca, finances_name as DescricaoFinanca, ""DueDate"" as DataVencimento,
                              ""Price"" as Valor, qtd_installments as QtdParcelas, paid as Pago
                             FROM finances 
                             WHERE finances_name ILIKE @descricao ";

            var parametros = new { descricao = $"%{descricao}%" };

            var financa = await _connection.QueryFirstOrDefaultAsync<FinancaDTO>(consulta, parametros);
            return financa;
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancas()
        {
            var consulta = @"SELECT 
                                id_finances as IdFinanca, finances_name as DescricaoFinanca, 
                                ""DueDate"" as DataVencimento,
                                ""Price"" as Valor, qtd_installments as QtdParcelas, paid as Pago
                             FROM finances ";

            var todasFinancas = await _connection.QueryAsync<FinancaDTO>(consulta);
            return todasFinancas;
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancasNaoPagas()
        {
            var consulta = @"SELECT 
                              id_finances as IdFinanca, finances_name as DescricaoFinanca, ""DueDate"" as DataVencimento,
                              ""Price"" as Valor, qtd_installments as QtdParcelas, paid as Pago
                             FROM finances 
                             WHERE paid = FALSE ";

            var todasFinancas = await _connection.QueryAsync<FinancaDTO>(consulta);
            return todasFinancas;
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancasPagas()
        {
            var consulta = @"SELECT 
                              id_finances as IdFinanca, finances_name as DescricaoFinanca, ""DueDate"" as DataVencimento,
                              ""Price"" as Valor, qtd_installments as QtdParcelas, paid as Pago
                             FROM finances 
                             WHERE paid = TRUE ";

            var todasFinancas = await _connection.QueryAsync<FinancaDTO>(consulta);
            return todasFinancas;
        }
    }
}
