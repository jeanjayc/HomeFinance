using HomeFinance.Domain.Models;
using HomeFinance.Infra.DTOs.Request.Financas;
using HomeFinance.Infra.DTOs.Response.Financas;

namespace HomeFinance.Infra.Mappings
{
    public static class FinancaMapping
    {
        public static FinancaDTO ToDto(Finances finance)
        {
            return new FinancaDTO
            {
                IdFinanca = finance.FinancaId,
                Titulo = finance.Titulo,
                DescricaoFinanca = finance.Descricao,
                DataVencimento = finance.DataVencimento,
                Valor = finance.Valor,
                Pago = finance.Pago,
                QtdParcelas = finance.QtdParcelas,
                Categoria = finance.Categoria,
                MesReferencia = finance.MesReferencia,
                TipoRecorrencia = finance.TipoRecorrencia,
                NumeroParcela = finance.NumeroParcela,
                DiaVencimento = finance.DiaVencimento,
                TemplateId = finance.TemplateId
            };
        }

        public static Finances ToEntity(FinancaCreateRequest request, DateTime dataVencimento)
        {
            return new Finances
            {
                Titulo = request.Titulo,
                Descricao = request.DescricaoFinanca,
                Valor = request.Valor,
                DataVencimento = dataVencimento,
                Pago = request.Pago,
                QtdParcelas = request.QtdParcelas,
                Categoria = request.Categoria,
                MesReferencia = request.MesReferencia,
                TipoRecorrencia = request.TipoRecorrencia,
                NumeroParcela = request.NumeroParcela,
                DiaVencimento = request.DiaVencimento,
                TemplateId = request.TemplateId
            };
        }

        public static DateTime ResolveDataVencimento(FinancaCreateRequest request)
        {
            if (request.DataVencimento != default)
                return request.DataVencimento.Date;

            // Agora request.MesReferencia � int, n�o precisa mais fazer split
            var year = DateTime.UtcNow.Year;
            var month = request.MesReferencia;

            // Ajustar ano se m�s j� passou
            if (month < DateTime.UtcNow.Month)
                year++;

            var day = request.DiaVencimento ?? 1;
            var daysInMonth = DateTime.DaysInMonth(year, month);
            if (day > daysInMonth)
                day = daysInMonth;

            return new DateTime(year, month, day);
        }
    }
}
