using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Repository;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class ShippersController : ControllerBase
{
    private readonly IShipperRepository _repository;

    public ShippersController(IShipperRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ShipperDto>> Get()
    {
        var shippers = _repository.GetAllShippers();
        return Ok(shippers);
    }
}