using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MiDbContext _context;

        public ProductRepository(MiDbContext context)
        {
            _context = context;
        }

        public List<ProductDto> GetAllProducts()
        {
            return _context.Products
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName
                })
                .ToList();
        }
    }
}
