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
                              f.idfinanca as IdFinanca, f.descricao as DescricaoFinanca, f.pago as Pago, 
                              f.data_vencimento as DataVencimento ,f.qtd_parcelas as QtdParcelas, f.valor as Valor
                             FROM financas f  
                             WHERE finances_name ILIKE @descricao ";

            var parametros = new { descricao = $"%{descricao}%" };

            var financa = await _connection.QueryFirstOrDefaultAsync<FinancaDTO>(consulta, parametros);
            return financa;
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancas()
        {
            var consulta = @"SELECT 
                              f.idfinanca as IdFinanca, f.descricao as DescricaoFinanca, f.pago as Pago, 
                              f.data_vencimento as DataVencimento ,f.qtd_parcelas as QtdParcelas, f.valor as Valor
                             FROM financas f ";

            var todasFinancas = await _connection.QueryAsync<FinancaDTO>(consulta);
            return todasFinancas;
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancasNaoPagas()
        {
            var consulta = @"SELECT 
                        f.idfinanca as IdFinanca, 
                        f.descricao as DescricaoFinanca, 
                        f.pago as Pago, 
                        f.data_vencimento as DataVencimento, 
                        f.qtd_parcelas as QtdParcelas, 
                        f.valor as Valor
                    FROM financas f 
                    WHERE f.pago = FALSE;";

            return await _connection.QueryAsync<FinancaDTO>(consulta);
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancasPagas()
        {
            var consulta = @"SELECT 
                              f.idfinanca as IdFinanca, f.descricao as DescricaoFinanca, f.pago as Pago, 
                              f.data_vencimento as DataVencimento ,f.qtd_parcelas as QtdParcelas, f.valor as Valor
                             FROM financas f  
                             WHERE paid = TRUE ";

            var todasFinancas = await _connection.QueryAsync<FinancaDTO>(consulta);
            return todasFinancas;
        }
    }
}
