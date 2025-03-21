// CustomerOrderPredictionsController.cs
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Repository;
using System.Collections.Generic;
using System.Linq;
using System; // Import System for StringComparison

[ApiController]
[Route("[controller]")]
public class CustomerOrderPredictionsController : ControllerBase
{
    private readonly ICustomerOrderPredictionRepository _repository;

    public CustomerOrderPredictionsController(ICustomerOrderPredictionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CustomerOrderPredictionDto>> Get(string search = null) // Accept search parameter
    {
        var predictions = _repository.GetCustomerOrderPredictions();

        if (!string.IsNullOrEmpty(search))
        {
            predictions = predictions.Where(p => p.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList(); // Filter by CustomerName
        }

        return Ok(predictions);
    }
}