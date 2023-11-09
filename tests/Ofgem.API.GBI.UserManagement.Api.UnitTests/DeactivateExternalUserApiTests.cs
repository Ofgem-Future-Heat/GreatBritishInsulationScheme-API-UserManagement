using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Exceptions;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using System.Net;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Api.UnitTests
{
	public class DeactivateExternalUserApiTests
	{
		private readonly Mock<IExternalUserService> _externalUserService;

		public DeactivateExternalUserApiTests()
		{
			_externalUserService = new Mock<IExternalUserService>();
		}

		[Fact]
		public async void DeactivateExternalUser_WhenUserExists_ReturnsNoContent()
		{
			// Arrange
			await using var application = new TestApplicationFactory(x =>
			{
				x.AddSingleton(_externalUserService.Object);
			});

			// Act
			using var client = application.CreateClient();
			var response = await client.PatchAsync($"/external-users/{Guid.NewGuid()}/deactivate", null);

			// Assert
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}

		[Fact]
		public async void DeactivateExternalUser_WhenUserDoesNotExist_ReturnsNotFound()
		{
			// Arrange
			await using var application = new TestApplicationFactory(x =>
			{
				x.AddSingleton(_externalUserService.Object);
			});

			_externalUserService.Setup(x => x.DeactivateExternalUser(It.IsAny<Guid>())).Throws<UserNotFoundException>();

			// Act
			using var client = application.CreateClient();
			var response = await client.PatchAsync($"/external-users/{Guid.NewGuid()}/deactivate", null);

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}
