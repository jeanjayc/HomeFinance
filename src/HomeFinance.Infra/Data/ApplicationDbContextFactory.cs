using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace HomeFinance.Infra.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=71321787;Host=localhost;Port=5432;Database=HomeFinance;Pooling=true;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
