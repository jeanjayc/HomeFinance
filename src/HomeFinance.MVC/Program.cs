using HomeFinance.Application.Interfaces;
using HomeFinance.Application.Services;
using HomeFinance.Infra.Data;
using HomeFinance.Infra.Identity.Data;
using HomeFinance.Infra.Interfaces;
using HomeFinance.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<IdentityDataContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddTransient<IFinanceRepository, FinancesRepository>();
builder.Services.AddTransient<IFinancesService, FinancesService>();

builder.Host.UseSerilog(((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration)));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Finances}/{action=Index}/{id?}");

app.Run();
