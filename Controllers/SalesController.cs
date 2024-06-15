using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesForecasting.Models;
using SalesForecasting.Services;
using SalesForecasting.Services.Interfaces;
using System.Linq;

namespace SalesForecasting.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesForecastingMvcContext _dbContext;
        private readonly ISalesService _salesService;

        public SalesController(SalesForecastingMvcContext dbContext,ISalesService salesService)
        {
            _dbContext = dbContext;
            _salesService = salesService;
        }

        public IActionResult SalesByYear(int year)
        {
           var salesByYear=_salesService.GetSalesByYear(year);

            ViewBag.year = year;
            ViewBag.totalsales= salesByYear.Sum(s => s.TotalSales);

            return View(salesByYear);
        }

        public IActionResult ApplyIncrement(int year, decimal percentageIncrement)
        {
            var SalesByYear = _salesService.GetSalesByYear(year);
            ViewBag.Year = year;
            ViewBag.PercentageIncrement = percentageIncrement;

            return View(SalesByYear);
        }

        public IActionResult ForecastedSales(int year, decimal percentageIncrease)
        {
            var forecastedSales =  _salesService.GetForecastedSalesByYearAsync(year, percentageIncrease);

            return Json(forecastedSales);
        }

        public async Task<IActionResult> SalesAndForecastedChart(int year, decimal percentageIncrease)
        {
            var salesByYear =  _salesService.GetSalesByYear(year);
            var forecastedSales =  _salesService.GetForecastedSalesByYearAsync(year, percentageIncrease);

            var combinedData = new
            {
                ActualSales = salesByYear,
                ForecastedSales = forecastedSales
            };

            return Json(combinedData);
        }
    }
}
