using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public interface IOrderRepository
    {
        List<OrderDto> GetOrdersByCustomerId(int customerId);
    }
}
