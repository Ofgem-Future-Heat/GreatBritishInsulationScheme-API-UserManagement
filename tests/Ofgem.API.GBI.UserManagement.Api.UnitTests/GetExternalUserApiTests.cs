using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using System.Net;
using Xunit;
using System.Text.Json;
using Ofgem.Database.GBI.Users.Domain.Enums;
using Ofgem.API.GBI.UserManagement.Application.Exceptions;

namespace Ofgem.API.GBI.UserManagement.Api.UnitTests
{
	public class GetExternalUserApiTests
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

		public GetExternalUserApiTests()
		{
			_externalUserService = new Mock<IExternalUserService>();
		}

        [Fact]
		public async void GetExternalUser_WithValidId_ReturnsUser()
		{
			// Arrange
			await using var application = new TestApplicationFactory(x =>
			{
				x.AddSingleton(_externalUserService.Object);
			});

			_externalUserService.Setup(x => x.GetExternalUser(It.IsAny<Guid>())).Returns(_externalUserDto);

			// Act
			using var client = application.CreateClient();
			var response = await client.GetAsync($"/external-users/{_externalUserDto.ExternalUserId}");

			string responseData = await response.Content.ReadAsStringAsync();
			var user = JsonSerializer.Deserialize<ExternalUserDto>(responseData);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.NotNull(user);
		}

		[Fact]
		public async void GetExternalUser_WithInvalidId_ReturnsNotFound()
		{
			// Arrange
			await using var application = new TestApplicationFactory(x =>
			{
				x.AddSingleton(_externalUserService.Object);
			});

			_externalUserService.Setup(x => x.GetExternalUser(It.IsAny<Guid>()))
				.Throws<UserNotFoundException>();

			// Act
			using var client = application.CreateClient();
			var response = await client.GetAsync($"/external-users/{_externalUserDto.ExternalUserId}");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}
