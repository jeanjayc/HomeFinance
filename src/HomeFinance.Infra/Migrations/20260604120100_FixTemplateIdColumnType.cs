using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    [Migration("20260604120100_FixTemplateIdColumnType")]
    public partial class FixTemplateIdColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM information_schema.columns
                        WHERE table_name = 'financas'
                          AND column_name = 'template_id'
                          AND data_type IN ('integer', 'bigint', 'smallint')
                    ) THEN
                        ALTER TABLE financas DROP COLUMN template_id;
                        ALTER TABLE financas ADD COLUMN template_id uuid NULL;
                    END IF;
                END $$;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "template_id",
                table: "financas");

            migrationBuilder.AddColumn<int>(
                name: "template_id",
                table: "financas",
                type: "integer",
                nullable: true);
        }
    }
}
