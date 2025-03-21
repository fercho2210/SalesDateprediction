using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Repository;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeesController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EmployeeDto>> Get()
    {
        var employees = _repository.GetAllEmployees();
        return Ok(employees);
    }
}