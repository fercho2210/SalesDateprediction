using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Repository;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductDto>> Get()
    {
        var products = _repository.GetAllProducts();
        return Ok(products);
    }
}