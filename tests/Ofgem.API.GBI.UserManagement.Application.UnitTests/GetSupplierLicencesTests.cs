using Microsoft.Extensions.Logging;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Services;
using Ofgem.Database.GBI.Users.Domain.Entities;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Application.UnitTests
{
    public class GetSupplierLicencesTests
    {
        private readonly Mock<ISupplierRepository> _supplierRepositoryMock;
        private readonly Mock<ILogger<SupplierService>> _loggerMock;

        private readonly IEnumerable<SupplierLicenceDto> _supplierLicences = new List<SupplierLicenceDto>()
        {
            new SupplierLicenceDto
            {
                SupplierLicenceReference = "EON02366970E",
                SupplierName = "EON",
                LicenceName = "E.ON UK plc",
                LicenceNo = "2366970",

            }
        };

        public GetSupplierLicencesTests()
        {
            _supplierRepositoryMock = new Mock<ISupplierRepository>();
            _loggerMock = new Mock<ILogger<SupplierService>>();
        }

        [Fact]
        public async Task GetSupplierLicences_WhenSupplierLicencesExist_ReturnsSuppliers()
        {
            // Arrange
            _supplierRepositoryMock.Setup(x => x.GetSupplierLicencesAsync()).Returns(Task.FromResult(_supplierLicences));
            var supplierService = new SupplierService(
                _supplierRepositoryMock.Object, _loggerMock.Object);

            // Act
            var supplierLicences = await supplierService.GetSupplierLicencesAsync();

            // Assert
            Assert.Single(supplierLicences);
        }

        [Fact]
        public async Task GetSupplierLicences_WhenSupplierLicencesIsEmpty_ReturnsSuppliers()
        {
            // Arrange
            IEnumerable<SupplierLicenceDto> emptyList = new List<SupplierLicenceDto>();
            _supplierRepositoryMock.Setup(x => x.GetSupplierLicencesAsync()).Returns(Task.FromResult(emptyList));
            var supplierService = new SupplierService(
                _supplierRepositoryMock.Object, _loggerMock.Object);

            // Act
            var supplierLicences = await supplierService.GetSupplierLicencesAsync();

            // Assert
            Assert.Empty(supplierLicences);
        }

        [Fact]
        public async Task GetSupplierLicences_WhenException_LogsAndThrowsException()
        {
            // Arrange
            IEnumerable<SupplierLicenceDto> emptyList = new List<SupplierLicenceDto>();
            _supplierRepositoryMock.Setup(x => x.GetSupplierLicencesAsync()).Throws<Exception>();
            var supplierService = new SupplierService(
                _supplierRepositoryMock.Object, _loggerMock.Object);

            // Act
            var actual = await Assert.ThrowsAsync<Exception>(async () => await supplierService.GetSupplierLicencesAsync());

            // Assert
            _loggerMock.Verify(
                           x => x.Log(
                               LogLevel.Error,
                               It.IsAny<EventId>(),
                               It.Is<It.IsAnyType>((o, t) => string.Equals($"Failed to retrieve supplier licences from database. Error: {actual.Message}", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                               It.IsAny<Exception>(),
                               It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                           Times.Once);
        }
    }
}
