using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ofgem.API.GBI.UserManagement.Api.UnitTests.Data;
using Ofgem.API.GBI.UserManagement.Application.Exceptions;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Models;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Enums;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Api.UnitTests
{
    public class SyncExternalUserApiTests
    {
        private readonly Mock<IExternalUserService> _externalUserService;

        private readonly ExternalUserDto _externalUserDto = new()
        {
            ExternalUserId = Guid.NewGuid(),
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "name@example.com",
            UserType = ExternalUserType.AuthorisedSignatoryRole,
            Supplier = new SupplierDto
            {
                SupplierId = 1,
                SupplierName = "OCT"
            }
        };

        public SyncExternalUserApiTests()
        {
            _externalUserService = new Mock<IExternalUserService>();
        }

        [Fact]
        public async void SyncExternalUser_WithNewProviderId_ReturnsUser()
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_externalUserService.Object);
            });

            _externalUserService.Setup(x => x.SyncExternalUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(_externalUserDto));

            var request = new SyncExternalUserRequest
            {
                EmailAddress = _externalUserDto.EmailAddress
            };

            // Act
            using var client = application.CreateClient();
            var response = await client.PutAsJsonAsync($"/external-users/{Guid.NewGuid()}/sync", request);

            string responseData = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<ExternalUserDto>(responseData);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(user);
        }

        [Fact]
        public async void SyncExternalUser_WithExistingProviderId_ReturnsUserWithUpdatedEmail()
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_externalUserService.Object);
            });

            _externalUserDto.UniqueUserId = Guid.NewGuid().ToString();
            _externalUserDto.EmailAddress = "name2@example.com";

            _externalUserService.Setup(x => x.SyncExternalUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(_externalUserDto));

            var request = new SyncExternalUserRequest
            {
                EmailAddress = _externalUserDto.EmailAddress
            };

            // Act
            using var client = application.CreateClient();
            var response = await client.PutAsJsonAsync($"/external-users/{_externalUserDto.UniqueUserId}/sync", request);

            string responseData = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<ExternalUserDto>(responseData, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(user);
            Assert.Equal(_externalUserDto.EmailAddress, user.EmailAddress);
        }

        [Fact]
        public async void SyncExternalUser_WhenUserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_externalUserService.Object);
            });

            _externalUserService.Setup(x => x.SyncExternalUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<UserNotFoundException>();

            var request = new SyncExternalUserRequest
            {
                EmailAddress = _externalUserDto.EmailAddress
            };

            // Act
            using var client = application.CreateClient();
            var response = await client.PutAsJsonAsync($"/external-users/{Guid.NewGuid()}/sync", request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [ClassData(typeof(InvalidEmailAddressTestData))]
        public async void SyncExternalUser_WithInvalidEmail_ReturnsBadRequest(string emailAddress)
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_externalUserService.Object);
            });

            var request = new SyncExternalUserRequest
            {
                EmailAddress = emailAddress
            };

            // Act
            using var client = application.CreateClient();
            var response = await client.PutAsJsonAsync($"/external-users/{Guid.NewGuid()}/sync", request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
