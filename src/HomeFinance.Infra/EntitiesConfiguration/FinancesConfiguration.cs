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

            builder.Property(f => f.Descricao)
                .HasColumnType("VARCHAR(70)")
                .HasColumnName("descricao")
                .IsRequired();

            builder.Property(f => f.Titulo)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("titulo");

            builder.Property(f => f.QtdParcelas)
                .HasColumnType("INTEGER")
                .HasColumnName("qtd_parcelas");

            builder.Property(f => f.Valor)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("valor")
                .IsRequired();

            builder.Property(f => f.DataVencimento)
                .HasColumnType("date")
                .HasColumnName("data_vencimento");

            builder.Property(f => f.Pago)
                .HasColumnType("BOOLEAN")
                .HasColumnName("pago");

            builder.Property(f => f.Categoria)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("categoria")
                .HasDefaultValue("expense");

            builder.Property(f => f.MesReferencia)
                .HasColumnType("INTEGER") 
                .HasColumnName("mes_referencia")
                .HasDefaultValue(DateTime.UtcNow.Month);

            builder.Property(f => f.TipoRecorrencia)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("tipo_recorrencia")
                .HasDefaultValue("single");

            builder.Property(f => f.NumeroParcela)
                .HasColumnType("INTEGER")
                .HasColumnName("numero_parcela");

            builder.Property(f => f.DiaVencimento)
                .HasColumnType("INTEGER")
                .HasColumnName("dia_vencimento");

            builder.Property(f => f.TemplateId)
                .HasColumnType("UUID")
                .HasColumnName("template_id");
        }
    }
}
