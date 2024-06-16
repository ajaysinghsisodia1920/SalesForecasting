using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesForecasting.Models;
using SalesForecasting.Services;
using SalesForecasting.Services.Interfaces;
using System.Linq;
using System.Text;

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
            ViewBag.TotalSales=SalesByYear.Sum(s => s.TotalSales); 
            ViewBag.PercentageIncrement = percentageIncrement;

            return View(SalesByYear);
        }

        public IActionResult ApplyIncrementOnState(int year, decimal percentageIncrement,string state)
        {
            var SalesByYear = _salesService.GetSalesByYear(year);
            var StateSales=SalesByYear.Find(s=>s.State == state);
            ViewBag.State = state;
            ViewBag.Year = year;
            ViewBag.PercentageIncrement=percentageIncrement;
            ViewBag.TotalSales = StateSales?.TotalSales;
            return View();
        }

        public IActionResult DownloadForecastedData(int year, decimal percentageIncrement)
        {
            var salesByYear = _salesService.GetSalesByYear(year);

            var forecastedData = salesByYear.Select(sales => new
            {
                State = sales.State,
                PercentageIncrease = percentageIncrement,
                SalesValue = sales.TotalSales * (1 + percentageIncrement / 100)
            }).ToList();

            var sb = new StringBuilder();
            sb.AppendLine("State,Percentage Increase,Sales Value");

            foreach (var data in forecastedData)
            {
                sb.AppendLine($"{data.State},{data.PercentageIncrease},{data.SalesValue}");
            }

            var csvData = Encoding.UTF8.GetBytes(sb.ToString());
            return File(csvData, "text/csv", "forecasted_data.csv");
        }

        public  IActionResult SalesAndForecastedChartByState(int year, decimal percentageIncrease)
        {
            ViewBag.Year = year;
            ViewBag.PercentageIncrease = percentageIncrease;
            return View();
        }

        [HttpGet]
        public  IActionResult GetSalesAndForecastedDataByState(int year, decimal percentageIncrease)
        {
            var salesByYear =  _salesService.GetSalesByYear(year);
            var forecastedSales =  _salesService.GetForecastedSalesByYear(year, percentageIncrease);

            var combinedData = new
            {
                ActualSales = salesByYear,
                ForecastedSales = forecastedSales
            };

            return Json(combinedData);
        }

        public IActionResult SalesAndForecastedChart(int year, decimal percentageIncrease)
        {
            ViewBag.Year = year;
            ViewBag.PercentageIncrease = percentageIncrease;
            return View();
        }

        public IActionResult GetSalesAndForecastedData(int year, decimal percentageIncrease)
        {
            var salesByYear = _salesService.GetSalesByYear(year);
            var forecastedSales = _salesService.GetForecastedSalesByYear(year, percentageIncrease);

            var combinedData = new
            {
                ActualSales = salesByYear.Sum(s => s.TotalSales),
                ForecastedSales = forecastedSales.Sum(s=> s.TotalSales)
            };

            return Json(combinedData);
        }
    }
}
