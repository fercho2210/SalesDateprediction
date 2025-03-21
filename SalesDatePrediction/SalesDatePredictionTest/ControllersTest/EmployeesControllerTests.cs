using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SalesDatePrediction.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly EmployeesController _controller;

        public EmployeesControllerTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _controller = new EmployeesController(_mockRepository.Object);
        }

        [Fact]
        public void Get_ReturnsOkResultWithEmployees()
        {
            // Arrange
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto { EmpId = 1, FirstName = "John", LastName = "Doe" },
                new EmployeeDto { EmpId = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _mockRepository.Setup(repo => repo.GetAllEmployees()).Returns(employees);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);
            Assert.Equal(employees.Count, returnedEmployees.Count());
        }

        [Fact]
        public void Get_ReturnsEmptyListWhenNoEmployees()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllEmployees()).Returns(new List<EmployeeDto>());

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);
            Assert.Empty(returnedEmployees);
        }

        [Fact]
        public void Get_CallsRepositoryGetAllEmployeesOnce()
        {
            // Act
            _controller.Get();

            // Assert
            _mockRepository.Verify(repo => repo.GetAllEmployees(), Times.Once);
        }
    }
}