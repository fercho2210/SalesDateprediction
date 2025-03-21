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
    public class OrderRepositoryTests
    {
        [Fact]
        public void GetOrdersByCustomerId_ReturnsCorrectOrderDtos()
        {
            // Arrange
            int customerId = 1;
            var orders = new List<Order>
            {
                new Order { OrderId = 1, CustId = 1, RequiredDate = DateTime.Now, ShippedDate = DateTime.Now.AddDays(1), ShipName = "Ship 1", ShipAddress = "Address 1", ShipCity = "City 1" },
                new Order { OrderId = 2, CustId = 1, RequiredDate = DateTime.Now.AddDays(2), ShippedDate = DateTime.Now.AddDays(3), ShipName = "Ship 2", ShipAddress = "Address 2", ShipCity = "City 2" },
                new Order { OrderId = 3, CustId = 2, RequiredDate = DateTime.Now.AddDays(4), ShippedDate = DateTime.Now.AddDays(5), ShipName = "Ship 3", ShipAddress = "Address 3", ShipCity = "City 3" } // Different customer
            }.AsQueryable();

            var mockContext = new Mock<MiDbContext>();
            mockContext.Setup(c => c.Orders).Returns(MockDbSet(orders));

            var repository = new OrderRepository(mockContext.Object);

            // Act
            var orderDtos = repository.GetOrdersByCustomerId(customerId);

            // Assert
            Assert.NotNull(orderDtos);
            Assert.Equal(2, orderDtos.Count); // Only orders for customerId 1

            var order1 = orderDtos.FirstOrDefault(o => o.OrderId == 1);
            Assert.NotNull(order1);
            Assert.Equal("Ship 1", order1.ShipName);

            var order2 = orderDtos.FirstOrDefault(o => o.OrderId == 2);
            Assert.NotNull(order2);
            Assert.Equal("Ship 2", order2.ShipName);
        }

        [Fact]
        public void GetOrdersByCustomerId_ReturnsEmptyListWhenNoOrdersForCustomer()
        {
            // Arrange
            int customerId = 3; // Customer with no orders
            var orders = new List<Order>
            {
                new Order { OrderId = 1, CustId = 1, RequiredDate = DateTime.Now, ShippedDate = DateTime.Now.AddDays(1), ShipName = "Ship 1", ShipAddress = "Address 1", ShipCity = "City 1" }
            }.AsQueryable();

            var mockContext = new Mock<MiDbContext>();
            mockContext.Setup(c => c.Orders).Returns(MockDbSet(orders));

            var repository = new OrderRepository(mockContext.Object);

            // Act
            var orderDtos = repository.GetOrdersByCustomerId(customerId);

            // Assert
            Assert.NotNull(orderDtos);
            Assert.Empty(orderDtos);
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