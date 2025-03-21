using Microsoft.EntityFrameworkCore;
using Moq;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SalesDatePrediction.Tests.Repositories
{
    public class CustomerOrderPredictionRepositoryTests
    {
        [Fact]
        public void GetCustomerOrderPredictions_ReturnsCorrectPredictions()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, CompanyName = "Customer A" },
                new Customer { CustId = 2, CompanyName = "Customer B" }
            }.AsQueryable();

            var orders = new List<Order>
            {
                new Order { OrderId = 1, CustId = 1, OrderDate = new DateTime(2023, 1, 1) },
                new Order { OrderId = 2, CustId = 1, OrderDate = new DateTime(2023, 1, 15) },
                new Order { OrderId = 3, CustId = 2, OrderDate = new DateTime(2023, 2, 1) }
            }.AsQueryable();

            var mockContext = new Mock<MiDbContext>();
            mockContext.Setup(c => c.Customers).Returns(MockDbSet(customers));
            mockContext.Setup(c => c.Orders).Returns(MockDbSet(orders));

            var repository = new CustomerOrderPredictionRepository(mockContext.Object);

            // Act
            var predictions = repository.GetCustomerOrderPredictions();

            // Assert
            Assert.NotNull(predictions);
            Assert.Equal(2, predictions.Count);

            var predictionA = predictions.FirstOrDefault(p => p.CustomerName == "Customer A");
            Assert.NotNull(predictionA);
            Assert.Equal(new DateTime(2023, 1, 15), predictionA.LastOrderDate);
            Assert.Equal(new DateTime(2023, 1, 29), predictionA.NextPredictedOrder); // 14 days average

            var predictionB = predictions.FirstOrDefault(p => p.CustomerName == "Customer B");
            Assert.NotNull(predictionB);
            Assert.Equal(new DateTime(2023, 2, 1), predictionB.LastOrderDate);
            Assert.Equal(new DateTime(2023, 2, 1), predictionB.NextPredictedOrder); // Only one order
        }

        private static DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}