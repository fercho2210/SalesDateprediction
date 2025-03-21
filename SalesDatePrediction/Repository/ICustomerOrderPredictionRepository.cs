using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public interface ICustomerOrderPredictionRepository
    {
        List<CustomerOrderPredictionDto> GetCustomerOrderPredictions();
    }
}
