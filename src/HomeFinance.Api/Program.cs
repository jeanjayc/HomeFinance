using HomeFinance.Application.Interfaces;
using HomeFinance.Application.Services;
using HomeFinance.Infra.Data;
using HomeFinance.Infra.Interfaces;
using HomeFinance.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddTransient<IFinanceRepository, FinancesRepository>();
builder.Services.AddTransient<IFinancesService, FinancesService>();

builder.Host.UseSerilog(((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration)));
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
