using HomeFinance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HomeFinance.Infra.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Finances> Finances { get; set; }
        public DbSet <Installments> Installments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseNpgsql("User ID=postgres;Password=71321787;Host=localhost;Port=5432;Database=HomeFinance;Pooling=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
