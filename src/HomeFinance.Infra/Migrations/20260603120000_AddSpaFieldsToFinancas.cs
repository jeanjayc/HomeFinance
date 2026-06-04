using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    [Migration("20260603120000_AddSpaFieldsToFinancas")]
    public partial class AddSpaFieldsToFinancas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "titulo",
                table: "financas",
                type: "VARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "categoria",
                table: "financas",
                type: "VARCHAR(10)",
                nullable: false,
                defaultValue: "expense");

            migrationBuilder.AddColumn<int>(
                name: "mes_referencia",
                table: "financas",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "tipo_recorrencia",
                table: "financas",
                type: "VARCHAR(20)",
                nullable: false,
                defaultValue: "single");

            migrationBuilder.AddColumn<int>(
                name: "numero_parcela",
                table: "financas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dia_vencimento",
                table: "financas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "template_id",
                table: "financas",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE financas
                SET mes_referencia = EXTRACT(MONTH FROM data_vencimento)::integer,
                    categoria = 'expense',
                    tipo_recorrencia = 'single'
                WHERE mes_referencia IS NULL OR mes_referencia < 1 OR mes_referencia > 12;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "titulo", table: "financas");
            migrationBuilder.DropColumn(name: "categoria", table: "financas");
            migrationBuilder.DropColumn(name: "mes_referencia", table: "financas");
            migrationBuilder.DropColumn(name: "tipo_recorrencia", table: "financas");
            migrationBuilder.DropColumn(name: "numero_parcela", table: "financas");
            migrationBuilder.DropColumn(name: "dia_vencimento", table: "financas");
            migrationBuilder.DropColumn(name: "template_id", table: "financas");
        }
    }
}
