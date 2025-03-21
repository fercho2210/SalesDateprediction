using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly MiDbContext _context;

        public ShipperRepository(MiDbContext context)
        {
            _context = context;
        }

        public List<ShipperDto> GetAllShippers()
        {
            return _context.Shippers
                .Select(s => new ShipperDto
                {
                    ShipperId = s.ShipperId,
                    CompanyName = s.CompanyName
                })
                .ToList();
        }
    }
}
