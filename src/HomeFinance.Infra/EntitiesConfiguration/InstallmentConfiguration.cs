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

            builder.HasKey(f => f.InstallmentId);
            builder.Property(f => f.DueDate).HasColumnType("date").HasColumnName("due_date").IsRequired();
            builder.Property(f => f.Price).HasColumnType("decimal").HasColumnName("amount").IsRequired();
            builder.Property(f => f.TotalInstallments).HasColumnType("INTEGER").HasColumnName("total_installments").IsRequired();
            builder.Property(i => i.InstallmentsPaid).HasColumnType("INTEGER").HasColumnName("installments_paid");
            builder.Property(i => i.Paid).HasColumnType("boolean").HasColumnName("paid").IsRequired();


        }
    }
}
