using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnpaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "paid",
                table: "finances",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paid",
                table: "finances");
        }
    }
}
