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
        private const string SelectColumns = @"
            f.idfinanca as IdFinanca,
            f.titulo as Titulo,
            f.descricao as DescricaoFinanca,
            f.pago as Pago,
            f.data_vencimento as DataVencimento,
            f.qtd_parcelas as QtdParcelas,
            f.valor as Valor,
            f.categoria as Categoria,
            f.mes_referencia as MesReferencia,
            f.tipo_recorrencia as TipoRecorrencia,
            f.numero_parcela as NumeroParcela,
            f.dia_vencimento as DiaVencimento,
            f.template_id as TemplateId";

        private readonly IDbConnection _connection;

        public FinancasDAO(IConfiguration configuration)
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("Connection"));
        }

        public async Task<FinancaDTO> ObterFinancaPorDescricao(string descricao)
        {
            var consulta = $@"SELECT {SelectColumns}
                             FROM financas f
                             WHERE f.descricao ILIKE @descricao ";

            var parametros = new { descricao = $"%{descricao}%" };

            var financa = await _connection.QueryFirstOrDefaultAsync<FinancaDTO>(consulta, parametros);
            return financa;
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancas()
        {
            var consulta = $@"SELECT {SelectColumns} FROM financas f";
            return await _connection.QueryAsync<FinancaDTO>(consulta);
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancasNaoPagas()
        {
            var consulta = $@"SELECT {SelectColumns}
                    FROM financas f
                    WHERE f.pago = FALSE";

            return await _connection.QueryAsync<FinancaDTO>(consulta);
        }

        public async Task<IEnumerable<FinancaDTO>> ObterTodasFinancasPagas()
        {
            var consulta = $@"SELECT {SelectColumns}
                             FROM financas f
                             WHERE f.pago = TRUE";

            return await _connection.QueryAsync<FinancaDTO>(consulta);
        }
    }
}
