using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addtableinstallments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentsPaid",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "TotalInstallments",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "due_date",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "pago",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "price",
                table: "finances");

            migrationBuilder.RenameColumn(
                name: "OWNER",
                table: "finances",
                newName: "owner");

            migrationBuilder.CreateTable(
                name: "installments",
                columns: table => new
                {
                    InstallmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    dueDate = table.Column<DateTime>(type: "date", nullable: false),
                    price = table.Column<decimal>(type: "decimal", nullable: false),
                    totalInstallments = table.Column<int>(name: "total Installments", type: "INTEGER", nullable: false),
                    installmentsPaid = table.Column<int>(name: "installments Paid", type: "INTEGER", nullable: false),
                    Paid = table.Column<bool>(type: "boolean", nullable: false),
                    FinancesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id_installment", x => x.InstallmentId);
                    table.ForeignKey(
                        name: "FK_installments_finances_FinancesId",
                        column: x => x.FinancesId,
                        principalTable: "finances",
                        principalColumn: "id_finances",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_installments_FinancesId",
                table: "installments",
                column: "FinancesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "installments");

            migrationBuilder.RenameColumn(
                name: "owner",
                table: "finances",
                newName: "OWNER");

            migrationBuilder.AddColumn<int>(
                name: "InstallmentsPaid",
                table: "finances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalInstallments",
                table: "finances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "due_date",
                table: "finances",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "pago",
                table: "finances",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "finances",
                type: "decimal",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
