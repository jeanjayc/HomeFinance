using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    [Migration("20260604120000_FixMesReferenciaColumnType")]
    public partial class FixMesReferenciaColumnType : Migration
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
                          AND column_name = 'mes_referencia'
                          AND data_type IN ('character varying', 'text')
                    ) THEN
                        ALTER TABLE financas
                        ALTER COLUMN mes_referencia TYPE integer
                        USING (
                            CASE
                                WHEN mes_referencia ~ '^\d{4}-\d{2}$'
                                    THEN CAST(SPLIT_PART(mes_referencia, '-', 2) AS integer)
                                WHEN mes_referencia ~ '^\d+$'
                                    THEN CAST(mes_referencia AS integer)
                                ELSE EXTRACT(MONTH FROM data_vencimento)::integer
                            END
                        );

                        ALTER TABLE financas
                        ALTER COLUMN mes_referencia SET DEFAULT EXTRACT(MONTH FROM CURRENT_DATE)::integer;

                        ALTER TABLE financas
                        ALTER COLUMN mes_referencia SET NOT NULL;
                    END IF;
                END $$;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "mes_referencia",
                table: "financas",
                type: "VARCHAR(7)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
