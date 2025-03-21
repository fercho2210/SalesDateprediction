using Microsoft.EntityFrameworkCore;
using Moq;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SalesDatePrediction.Tests.Repositories
{
    public class EmployeeRepositoryTests
    {
       
        [Fact]
        public void GetAllEmployees_ReturnsEmptyListWhenNoEmployees()
        {
            // Arrange
            var emptyEmployees = new List<Employee>().AsQueryable();

            var mockContext = new Mock<MiDbContext>();
            mockContext.Setup(c => c.Employees).Returns(MockDbSet(emptyEmployees));

            var repository = new EmployeeRepository(mockContext.Object);

            // Act
            var employeeDtos = repository.GetAllEmployees();

            // Assert
            Assert.NotNull(employeeDtos);
            Assert.Empty(employeeDtos);
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