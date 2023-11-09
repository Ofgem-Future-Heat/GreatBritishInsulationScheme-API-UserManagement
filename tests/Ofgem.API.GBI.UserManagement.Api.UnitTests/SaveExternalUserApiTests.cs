using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ofgem.API.GBI.UserManagement.Api.UnitTests.Data;
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
    public class SaveExternalUserApiTests
    {
        private readonly Mock<IExternalUserService> _externalUserService;

        private readonly SaveExternalUserRequest _createExternalUserRequest;

        public SaveExternalUserApiTests()
        {
            _externalUserService = new Mock<IExternalUserService>();

            _createExternalUserRequest = new()
            {
                ExternalUserId = Guid.Empty,
                SupplierId = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                EmailAddress = "name@example.com",
                UserType = ExternalUserType.AuthorisedSignatoryRole
            };
        }

        [Fact]
        public async void SaveExternalUser_WithValidRequest_ReturnsSuccess()
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_externalUserService.Object);
            });

            _externalUserService.Setup(x => x.SaveExternalUserAsync(It.IsAny<SaveExternalUserRequest>()))
                .Returns(Task.FromResult(new ExternalUserDto()));

            // Act
            using var client = application.CreateClient();
            var response = await client.PutAsJsonAsync("/external-users", _createExternalUserRequest);

            string responseData = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<Dictionary<string, Guid>>(responseData);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(user);
            Assert.True(user.ContainsKey("externalUserId"));
        }

        [Theory]
        [ClassData(typeof(InvalidNameTestData))]
        public async void SaveExternalUser_WithInvalidFirstName_ReturnsBadRequest(string firstName)
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_externalUserService.Object);
            });

            _createExternalUserRequest.FirstName = firstName;

            // Act
            using var client = application.CreateClient();
            var response = await client.PutAsJsonAsync("/external-users", _createExternalUserRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [ClassData(typeof(InvalidNameTestData))]
        public async void SaveExternalUser_WithInvalidLastName_ReturnsBadRequest(string lastName)
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_externalUserService.Object);
            });

            _createExternalUserRequest.LastName = lastName;

            // Act
            using var client = application.CreateClient();
            var response = await client.PutAsJsonAsync("/external-users", _createExternalUserRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [ClassData(typeof(InvalidEmailAddressTestData))]
        public async void SaveExternalUser_WithInvalidEmailAddress_ReturnsBadRequest(string emailAddress)
        {
            // Arrange
            await using var application = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_externalUserService.Object);
            });

            _createExternalUserRequest.EmailAddress = emailAddress;

            // Act
            using var client = application.CreateClient();
            var response = await client.PutAsJsonAsync("/external-users", _createExternalUserRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
