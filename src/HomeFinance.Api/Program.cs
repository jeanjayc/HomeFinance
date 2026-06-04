using Dapper;
using HomeFinance.Application.Interfaces;
using HomeFinance.Application.Services;
using HomeFinance.Infra.DAO;
using HomeFinance.Infra.Data;
using HomeFinance.Infra.Identity.Data;
using HomeFinance.Infra.Identity.Service;
using HomeFinance.Infra.Interfaces;
using HomeFinance.Infra.Interfaces.DAO;
using HomeFinance.Infra.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var connectionString = builder.Configuration.GetConnectionString("Connection")
    ?? throw new InvalidOperationException("Connection string 'Connection' is not configured.");

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<IdentityDataContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>()
    .AddDefaultTokenProviders();

DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddTransient<IFinanceRepository, FinancesRepository>();
builder.Services.AddTransient<IFinancesService, FinancesService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IFinancaDAO, FinancasDAO>();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:3000"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpa", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var appDb = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDb.Database.Migrate();

    var identityDb = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();
    identityDb.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpa");
app.UseAuthorization();
app.MapControllers();

app.Run();
