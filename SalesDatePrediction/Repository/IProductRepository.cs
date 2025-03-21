using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public interface IProductRepository
    {
        List<ProductDto> GetAllProducts();
    }
}
