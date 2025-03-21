using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Repository;

[ApiController]
[Route("[controller]")]
public class OrdersCreateController : ControllerBase
{
    private readonly IOrdersCreateRepository _repository;

    public OrdersCreateController(IOrdersCreateRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public ActionResult<int> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
    {
        int orderId = _repository.CreateOrder(orderCreateDto);
        return Ok(orderId);
    }
}