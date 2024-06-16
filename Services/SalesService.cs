using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesForecasting.Models;
using SalesForecasting.Services.Interfaces;
using System.Data;

namespace SalesForecasting.Services
{
    public class SalesService : ISalesService
    {
        private readonly SalesForecastingMvcContext _dbContext;

        public SalesService(SalesForecastingMvcContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<SalesByState> GetSalesByYear(int year)
        {
            var SalesByYear = (from Order in _dbContext.Orders
                               join Product in _dbContext.Products
                               on Order.OrderId equals Product.OrderId
                               select new
                               {
                                   Order,
                                   Product
                               }).AsEnumerable()
                                .Where(ti => ti.Order.OrderDate.Substring(ti.Order.OrderDate.LastIndexOf('/') + 1) == (year % 100).ToString())
                                .GroupBy(ti => ti.Order.State)
                                .Select(g => new SalesByState
                                {
                                    State = g.Key,
                                    TotalSales = (decimal)g.Sum(s => s.Product.Sales)
                                }).ToList();
                                //group Product by Order.State into os
                                //                   select new SalesByState
                                //                   {
                                //                       State = os.Key,
                                //                       TotalSales = (decimal)os.Sum(s => s.Sales)
                                //                   }).ToList();
            var ReturnByYear = (from Product in _dbContext.Products
                                join OrderReturn in _dbContext.OrderReturns on Product.OrderId equals OrderReturn.OrderId
                                join Order in _dbContext.Orders on OrderReturn.OrderId equals Order.OrderId
                                select new
                                {
                                    Product,
                                    Order,
                                    OrderReturn
                                }).AsEnumerable()
                                .Where(ti => ti.Order.OrderDate.Substring(ti.Order.OrderDate.LastIndexOf('/') + 1) == (year % 100).ToString())
                                .GroupBy(ti => ti.Order.State)
                                .Select(g => new SalesByState
                                {
                                    State = g.Key,
                                    TotalSales = (decimal)g.Sum(s => s.Product.Sales)
                                }).ToList();
                                //   group Product by Order.State into os
                                //  select new SalesByState
                                //  {
                                //      State = os.Key,
                                //      TotalSales = (decimal)os.Sum(s => s.Sales)
                                //}).ToList();
            foreach (var item in SalesByYear)
            {
                var ret = ReturnByYear.FirstOrDefault(r => r.State == item.State);
                if (ret != null)
                {
                    item.TotalSales -= ret.TotalSales;
                }
            }

            return SalesByYear;

        }


        public IEnumerable<SalesByState> GetForecastedSalesByYear(int year, decimal percentageIncrease)
        {
            var salesByYear = GetSalesByYear(year);

            var forecastedSales = salesByYear.Select(s => new SalesByState
            {
                State = s.State,
                TotalSales = s.TotalSales * (1 + percentageIncrease / 100)
            });

            return forecastedSales;
        }

        public IEnumerable<string> GetAllStates()
        {
            return _dbContext.Orders.Select(s => s.State).Distinct().ToList<string>();
        }
    }

    public class SalesByState
    {
        public string State { get; set; }
        public decimal TotalSales { get; set; }
    }
}
