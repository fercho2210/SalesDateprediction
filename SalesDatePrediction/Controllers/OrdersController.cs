using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Repository;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repository;

    public OrdersController(IOrderRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{customerId}")]
    public ActionResult<IEnumerable<OrderDto>> Get(int customerId)
    {
        var orders = _repository.GetOrdersByCustomerId(customerId);
        return Ok(orders);
    }
}