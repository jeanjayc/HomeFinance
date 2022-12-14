// <auto-generated />
using System;
using HomeFinance.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeFinance.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221117003941_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeFinance.Domain.Models.Finances", b =>
                {
                    b.Property<Guid>("FinancesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_finances");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("date")
                        .HasColumnName("due_date");

                    b.Property<string>("FinanceName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(70)")
                        .HasColumnName("finances_name");

                    b.Property<bool>("Pago")
                        .HasColumnType("boolean")
                        .HasColumnName("pago");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal")
                        .HasColumnName("price");

                    b.HasKey("FinancesId")
                        .HasName("id_finances");

                    b.ToTable("finances", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
