using SalesDatePrediction.Dto;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repository
{
    public class OrdersCreateRepository : IOrdersCreateRepository
    {
        private readonly MiDbContext _context;

        public OrdersCreateRepository(MiDbContext context)
        {
            _context = context;
        }

        public int CreateOrder(OrderCreateDto orderCreateDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Inserción de la orden
                    var order = new Order
                    {
                        EmpId = orderCreateDto.EmpId,
                        ShipperId = orderCreateDto.ShipperId,
                        ShipName = orderCreateDto.ShipName,
                        ShipAddress = orderCreateDto.ShipAddress,
                        ShipCity = orderCreateDto.ShipCity,
                        OrderDate = orderCreateDto.OrderDate,
                        RequiredDate = orderCreateDto.RequiredDate,
                        ShippedDate = orderCreateDto.ShippedDate,
                        Freight = orderCreateDto.Freight,
                        ShipCountry = orderCreateDto.ShipCountry,
                        CustId = orderCreateDto.CustId // Permitir CustId nulo
                    };

                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    // Obtención del ID de la orden recién creada
                    int orderId = order.OrderId;

                    // Inserción del detalle de la orden
                    var orderDetail = new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = orderCreateDto.OrderDetail.ProductId,
                        UnitPrice = orderCreateDto.OrderDetail.UnitPrice,
                        Qty = orderCreateDto.OrderDetail.Qty,
                        Discount = orderCreateDto.OrderDetail.Discount
                    };

                    _context.OrderDetails.Add(orderDetail);
                    _context.SaveChanges();

                    transaction.Commit();
                    return orderId;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}