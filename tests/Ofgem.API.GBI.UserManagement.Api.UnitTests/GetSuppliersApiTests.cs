using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Api.UnitTests
{
    public class GetSuppliersApiTests
    {
        private readonly Mock<ISupplierService> _supplierService;

        private readonly IEnumerable<SupplierDto> _suppliers = new List<SupplierDto>()
        {
            new SupplierDto
            {
                SupplierId = 1,
                SupplierName = "EON"
            }
        };

        public GetSuppliersApiTests()
        {
            _supplierService = new Mock<ISupplierService>();
        }

        [Fact]
        public async void GetSuppliers_WhenSuppliersExist_ReturnsSuppliers()
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_supplierService.Object);
            });

            _supplierService.Setup(x => x.GetSuppliers()).Returns(_suppliers);

            // Act
            using var client = application.CreateClient();
            var response = await client.GetAsync($"/suppliers");

            string responseData = await response.Content.ReadAsStringAsync();
            var suppliers = JsonSerializer.Deserialize<List<SupplierDto>>(responseData) ?? new List<SupplierDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Single(suppliers);
        }
    }
}
