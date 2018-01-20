using Microsoft.AspNetCore.Mvc;
using Moq;
using RepositorySample.Framework;
using RepositorySample.Service.Controllers;
using RepositorySample.Service.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RepositorySample.Tests
{
    public class CustomersControllerTests
    {
        [Fact]
        public async Task GetAllCustomerTestAsync()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), Name = "Customer1" },
                new Customer { Id = Guid.NewGuid(), Name = "Customer2" }
            };

            var mockRepository = new Mock<IRepository<Customer>>();
            mockRepository.Setup(x => x.FindBySpecificationAsync(It.IsAny<Expression<Func<Customer, bool>>>(), It.IsAny<CancellationToken>()))
                .Returns<Expression<Func<Customer, bool>>, CancellationToken>((expr, token) =>
                {
                    return Task.FromResult(customers.Where(expr.Compile()));
                });

            var mockRepositoryContext = new Mock<IRepositoryContext>();
            mockRepositoryContext.Setup(x => x.GetRepository<Customer>()).Returns(mockRepository.Object);

            // Act
            var customersController = new CustomersController(mockRepositoryContext.Object);
            var objectResult = await customersController.GetAllAsync();

            // Assert
            Assert.IsType<OkObjectResult>(objectResult);

            var foundCustomers = (objectResult as OkObjectResult).Value as IEnumerable<Customer>;
            Assert.Equal(2, foundCustomers.Count());
        }
    }
}
