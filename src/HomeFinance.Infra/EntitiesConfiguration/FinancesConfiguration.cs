using HomeFinance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeFinance.Infra.EntitiesConfiguration
{
    public class FinancesConfiguration : IEntityTypeConfiguration<Finances>
    {
        public void Configure(EntityTypeBuilder<Finances> builder)
        {
            builder.ToTable("financas");

            builder.HasKey(f => f.FinancaId).HasName("idfinanca");
            builder.Property(f => f.Descricao).HasColumnType("VARCHAR(70)").HasColumnName("descricao").IsRequired();
            builder.Property(f => f.QtdParcelas).HasColumnType("BIGINT").HasColumnName("qtd_parcelas");
            builder.Property(f => f.Valor).HasColumnType("decimal(10,2)").HasColumnName("valor").IsRequired();
            builder.Property(f => f.DataVencimento).HasColumnType("date").HasColumnName("data_vencimento");
            builder.Property(f => f.Pago).HasColumnType("BOOLEAN").HasColumnName("pago");
        }
    }
}
