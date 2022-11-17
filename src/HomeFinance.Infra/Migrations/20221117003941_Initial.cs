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
                    duedate = table.Column<DateTime>(name: "due_date", type: "date", nullable: false),
                    price = table.Column<decimal>(type: "decimal", nullable: false),
                    pago = table.Column<bool>(type: "boolean", nullable: false)
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
