using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajusteclassefinances : Migration
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
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    InstallmentsPaid = table.Column<int>(type: "integer", nullable: true),
                    qtdinstallments = table.Column<long>(name: "qtd_installments", type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id_finances", x => x.idfinances);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "finances");
        }
    }
}
