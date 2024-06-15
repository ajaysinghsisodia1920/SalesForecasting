namespace SalesForecasting.Services.Interfaces
{
    public interface ISalesService
    {
        List<SalesByState> GetSalesByYear(int year);
        IEnumerable<SalesByState> GetForecastedSalesByYearAsync(int year, decimal percentageIncrease);
    }
}
