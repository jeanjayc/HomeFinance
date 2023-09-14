using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "finances",
                columns: table => new
                {
                    idfinances = table.Column<Guid>(name: "id_finances", type: "uuid", nullable: false),
                    financesname = table.Column<string>(name: "finances_name", type: "VARCHAR(70)", nullable: false),
                    qtdinstallments = table.Column<long>(name: "qtd_installments", type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id_finances", x => x.idfinances);
                });

            migrationBuilder.CreateTable(
                name: "installments",
                columns: table => new
                {
                    idinstallment = table.Column<Guid>(name: "id_installment", type: "uuid", nullable: false),
                    duedate = table.Column<DateTime>(name: "due_date", type: "date", nullable: false),
                    amount = table.Column<decimal>(type: "decimal", nullable: false),
                    totalinstallments = table.Column<int>(name: "total_installments", type: "INTEGER", nullable: false),
                    installmentspaid = table.Column<int>(name: "installments_paid", type: "INTEGER", nullable: false),
                    paid = table.Column<bool>(type: "boolean", nullable: false),
                    financeid = table.Column<Guid>(name: "finance_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_installments", x => x.idinstallment);
                    table.ForeignKey(
                        name: "FK_installments_finances_finance_id",
                        column: x => x.financeid,
                        principalTable: "finances",
                        principalColumn: "id_finances",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_installments_finance_id",
                table: "installments",
                column: "finance_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "installments");

            migrationBuilder.DropTable(
                name: "finances");
        }
    }
}
