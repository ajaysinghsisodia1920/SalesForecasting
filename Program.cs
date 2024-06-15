using Microsoft.EntityFrameworkCore;
using SalesForecasting.Models;
using SalesForecasting.Services;
using SalesForecasting.Services.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<SalesForecastingMvcContext>(options => options.UseSqlServer(configuration.GetConnectionString("SalesForecastingMvcContext")));
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
