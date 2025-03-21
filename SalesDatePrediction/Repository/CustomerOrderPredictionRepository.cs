using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    // Repositories/CustomerOrderPredictionRepository.cs
    public class CustomerOrderPredictionRepository : ICustomerOrderPredictionRepository
    {
        private readonly MiDbContext _context;

        public CustomerOrderPredictionRepository(MiDbContext context)
        {
            _context = context;
        }

        public List<CustomerOrderPredictionDto> GetCustomerOrderPredictions()
        {
            var customerOrders = _context.Customers
                .Join(_context.Orders, c => c.CustId, o => o.CustId, (c, o) => new { c, o })
                .GroupBy(co => co.c.CustId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    CustomerName = g.First().c.CompanyName,
                    Orders = g.OrderBy(co => co.o.OrderDate).Select(co => co.o.OrderDate).ToList()
                })
                .ToList();

            var results = customerOrders.Select(co =>
            {
                var lastOrderDate = co.Orders.Last();
                double averageDays = 0;

                if (co.Orders.Count > 1)
                {
                    var dateDiffs = new List<double>();
                    for (int i = 1; i < co.Orders.Count; i++)
                    {
                        dateDiffs.Add((co.Orders[i] - co.Orders[i - 1]).TotalDays);
                    }
                    averageDays = dateDiffs.Average();
                }

                var nextPredictedOrder = lastOrderDate.AddDays(averageDays);

                return new CustomerOrderPredictionDto
                {
                    CustomerId = co.CustomerId,
                    CustomerName = co.CustomerName,
                    LastOrderDate = lastOrderDate,
                    NextPredictedOrder = nextPredictedOrder
                };
            }).OrderBy(dto => dto.CustomerName).ToList();

            return results;
        }
    }
}
