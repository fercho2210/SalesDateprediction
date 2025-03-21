using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit; // Agrega esta línea

namespace SalesDatePrediction.Tests.Controllers
{
    public class CustomerOrderPredictionsControllerTests
    {
        private readonly Mock<ICustomerOrderPredictionRepository> _mockRepository;
        private readonly CustomerOrderPredictionsController _controller;

        public CustomerOrderPredictionsControllerTests()
        {
            _mockRepository = new Mock<ICustomerOrderPredictionRepository>();
            _controller = new CustomerOrderPredictionsController(_mockRepository.Object);
        }

        [Fact]
        public void Get_ReturnsOkResultWithPredictions()
        {
            // Arrange
            var predictions = new List<CustomerOrderPredictionDto>
            {
                new CustomerOrderPredictionDto { CustId = 1, PredictedOrderDate = new System.DateTime(2024, 1, 15) },
                new CustomerOrderPredictionDto { CustId = 2, PredictedOrderDate = new System.DateTime(2024, 2, 20) }
            };

            _mockRepository.Setup(repo => repo.GetCustomerOrderPredictions()).Returns(predictions);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPredictions = Assert.IsAssignableFrom<IEnumerable<CustomerOrderPredictionDto>>(okResult.Value);
            Assert.Equal(predictions.Count, returnedPredictions.Count());
        }

        [Fact]
        public void Get_ReturnsEmptyListWhenNoPredictions()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetCustomerOrderPredictions()).Returns(new List<CustomerOrderPredictionDto>());

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPredictions = Assert.IsAssignableFrom<IEnumerable<CustomerOrderPredictionDto>>(okResult.Value);
            Assert.Empty(returnedPredictions);
            }

            [Fact]
        public void Get_CallsRepositoryGetCustomerOrderPredictionsOnce()
        {
            // Act
            _controller.Get();

            // Assert
            _mockRepository.Verify(repo => repo.GetCustomerOrderPredictions(), Times.Once);
        }
    }
}