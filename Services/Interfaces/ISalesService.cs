namespace SalesForecasting.Services.Interfaces
{
    public interface ISalesService
    {
        List<SalesByState> GetSalesByYear(int year);
        IEnumerable<SalesByState> GetForecastedSalesByYear(int year, decimal percentageIncrease);

        IEnumerable<string> GetAllStates();
    }
}
