using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MiDbContext _context;

        public OrderRepository(MiDbContext context)
        {
            _context = context;
        }

        public List<OrderDto> GetOrdersByCustomerId(int customerId)
        {
            return _context.Orders
                .Where(o => o.CustId == customerId)
                .Select(o => new OrderDto
                {
                    customerId = o.CustId,
                    OrderId = o.OrderId,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    ShipName = o.ShipName,
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity
                })
                .ToList();
        }
    }
}
