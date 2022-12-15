using HomeFinance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeFinance.Infra.EntitiesConfiguration
{
    public class InstallmentConfiguration : IEntityTypeConfiguration<Installments>
    {
        public void Configure(EntityTypeBuilder<Installments> builder)
        {
            builder.ToTable("installments");

            builder.HasKey(f => f.InstallmentId).HasName("id_installment");
            builder.Property(f => f.DueDate).HasColumnType("date").HasColumnName("dueDate").IsRequired();
            builder.Property(f => f.Price).HasColumnType("decimal").HasColumnName("price").IsRequired();
            builder.Property(f => f.TotalInstallments).HasColumnType("INTEGER").HasColumnName("total Installments").IsRequired();
            builder.Property(i => i.InstallmentsPaid).HasColumnType("INTEGER").HasColumnName("installments Paid");
            builder.Property(i => i.Paid).HasColumnType("boolean").HasColumnName("Paid").IsRequired();

                
        }
    }
}
