using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RenomearTabelaEColunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "id_finances",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "InstallmentsPaid",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "qtd_installments",
                table: "finances");

            migrationBuilder.RenameTable(
                name: "finances",
                newName: "financas");

            migrationBuilder.RenameColumn(
                name: "paid",
                table: "financas",
                newName: "pago");

            migrationBuilder.RenameColumn(
                name: "finances_name",
                table: "financas",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "id_finances",
                table: "financas",
                newName: "idfinanca");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_vencimento",
                table: "financas",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "qtd_parcelas",
                table: "financas",
                type: "BIGINT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "valor",
                table: "financas",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "idfinanca",
                table: "financas",
                column: "idfinanca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "idfinanca",
                table: "financas");

            migrationBuilder.DropColumn(
                name: "data_vencimento",
                table: "financas");

            migrationBuilder.DropColumn(
                name: "qtd_parcelas",
                table: "financas");

            migrationBuilder.DropColumn(
                name: "valor",
                table: "financas");

            migrationBuilder.RenameTable(
                name: "financas",
                newName: "finances");

            migrationBuilder.RenameColumn(
                name: "pago",
                table: "finances",
                newName: "paid");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "finances",
                newName: "finances_name");

            migrationBuilder.RenameColumn(
                name: "idfinanca",
                table: "finances",
                newName: "id_finances");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "finances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InstallmentsPaid",
                table: "finances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "finances",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "qtd_installments",
                table: "finances",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "id_finances",
                table: "finances",
                column: "id_finances");
        }
    }
}
