using HomeFinance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeFinance.Infra.EntitiesConfiguration
{
    public class FinancesConfiguration : IEntityTypeConfiguration<Finances>
    {
        public void Configure(EntityTypeBuilder<Finances> builder)
        {
            builder.ToTable("finances");

            builder.HasKey(f => f.FinancesId).HasName("id_finances");
            builder.Property(f => f.FinanceName).HasColumnType("VARCHAR(70)").HasColumnName("finances_name").IsRequired();
            builder.Property(f => f.DueDate).HasColumnType("date").HasColumnName("due_date").IsRequired();
            builder.Property(f => f.Price).HasColumnType("decimal").HasColumnName("price").IsRequired();
        }
    }
}
