using Microsoft.Extensions.Logging;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Services;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Application.UnitTests
{
    public class GetSuppliersTests
    {
        private readonly Mock<ISupplierRepository> _supplierRepositoryMock;
        private readonly Mock<ILogger<SupplierService>> _loggerMock;

        private readonly IEnumerable<SupplierDto> _suppliers = new List<SupplierDto>()
        {
            new SupplierDto
            {
                SupplierId = 1,
                SupplierName = "EON"
            }
        };

        public GetSuppliersTests()
        {
            _supplierRepositoryMock = new Mock<ISupplierRepository>();
            _loggerMock = new Mock<ILogger<SupplierService>>();
        }

        [Fact]
        public void GetSuppliers_WhenSuppliersExist_ReturnsSuppliers()
        {
            // Arrange
            _supplierRepositoryMock.Setup(x => x.GetSuppliers()).Returns(_suppliers);
            var supplierService = new SupplierService(
                _supplierRepositoryMock.Object, _loggerMock.Object);

            // Act
            IEnumerable<SupplierDto> suppliers = supplierService.GetSuppliers();

            // Assert
            Assert.Single(suppliers);
        }


        [Fact]
        public void GetSuppliers_WhenSuppliersIsEmpty_ReturnsSuppliers()
        {
            // Arrange
            IEnumerable<SupplierDto> emptyList = new List<SupplierDto>();
            _supplierRepositoryMock.Setup(x => x.GetSuppliers()).Returns(emptyList);
            var supplierService = new SupplierService(
                _supplierRepositoryMock.Object, _loggerMock.Object);

            // Act
            var suppliers = supplierService.GetSuppliers();

            // Assert
            Assert.Empty(suppliers);
        }

        [Fact]
        public void GetSuppliers_WhenException_LogsAndThrowsException()
        {
            // Arrange
            IEnumerable<SupplierDto> emptyList = new List<SupplierDto>();
            _supplierRepositoryMock.Setup(x => x.GetSuppliers()).Throws<Exception>();
            var supplierService = new SupplierService(
                _supplierRepositoryMock.Object, _loggerMock.Object);

            // Act
            var actual = Assert.Throws<Exception>(() => supplierService.GetSuppliers());

            // Assert
            _loggerMock.Verify(
                           x => x.Log(
                               LogLevel.Error,
                               It.IsAny<EventId>(),
                               It.Is<It.IsAnyType>((o, t) => string.Equals($"Failed to retrieve suppliers from database. Error: {actual.Message}", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                               It.IsAny<Exception>(),
                               It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                           Times.Once);
        }
    }
}
