using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public interface IShipperRepository
    {
        List<ShipperDto> GetAllShippers();
    }
}
