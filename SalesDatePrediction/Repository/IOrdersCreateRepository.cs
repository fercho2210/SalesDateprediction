using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public interface IOrdersCreateRepository
    {
        int CreateOrder(OrderCreateDto orderCreateDto);
    }
}
