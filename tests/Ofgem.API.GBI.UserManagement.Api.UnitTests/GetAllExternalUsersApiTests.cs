using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Enums;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Api.UnitTests;

public class GetAllExternalUsersApiTests
{
    private readonly Mock<IExternalUserService> _userService;
	private readonly JsonSerializerOptions _serializerOptions;
	private readonly List<ExternalUserDto> _users;

    public GetAllExternalUsersApiTests()
    {
        _userService = new Mock<IExternalUserService>();

        _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        _users = new List<ExternalUserDto>
        {
			new ExternalUserDto
		    {
			    ExternalUserId = Guid.NewGuid(),
			    UniqueUserId = "blank",
			    FirstName = "firstname",
			    LastName = "lastname",
			    EmailAddress = "firstname@aol.com",
			    UserType = ExternalUserType.AuthorisedSignatoryRole,
			    IsActive = false,
                Supplier = new SupplierDto
                {
                    SupplierId = 1,
                    SupplierName = "SSE"
                }
		    },
			new ExternalUserDto
		    {
                ExternalUserId = Guid.NewGuid(),
			    UniqueUserId = "",
			    FirstName = "name",
			    LastName = "lname",
			    EmailAddress = "name@test.com",
			    UserType = ExternalUserType.AdditionalUserRole,
			    IsActive = true,
				Supplier = new SupplierDto
				{
                    SupplierId = 2,
                    SupplierName = "EDF"
				}
			},
			new ExternalUserDto
		    {
                ExternalUserId = Guid.NewGuid(),
			    UniqueUserId = "00",
			    FirstName = "first",
			    LastName = "name",
			    EmailAddress = "first@first.co.uk",
			    UserType = ExternalUserType.AdditionalUserRole,
			    IsActive = false,
				Supplier = new SupplierDto
				{
                    SupplierId = 3,
                    SupplierName = "NPower"
				}
			}
		};
    }

    [Fact]
    public async void GetAllUsersRequest_WithEmptyDB_ReturnsOkResponseWithEmptyUsersList()
    {
        // Arrange
        await using var application = new TestApplicationFactory(x => { x.AddSingleton(_userService.Object); });
        _userService.Setup(x => x.ReadAllExternalUsersAsync()).Returns(Task.FromResult(new List<ExternalUserDto>()));
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/external-users");
        string responseData = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(responseData);
    }

    [Fact]
    public async void GetAllUsersRequest_ReturnsListOfAllUsers()
    {

        // Arrange
        await using var application = new TestApplicationFactory(x => { x.AddSingleton(_userService.Object); });
        _userService.Setup(x => x.ReadAllExternalUsersAsync()).Returns(Task.FromResult(_users));
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/external-users");
        string responseData = await response.Content.ReadAsStringAsync();
        List<ExternalUserDto>? responseList = JsonSerializer.Deserialize<List<ExternalUserDto>>(responseData, _serializerOptions);

        // Assert
        for (int i = 0; i < responseList?.Count; i++)
        {
            Assert.True(responseList[i].ExternalUserId.Equals(_users[i].ExternalUserId));
            Assert.True(responseList[i].UniqueUserId.Equals(_users[i].UniqueUserId));
            Assert.True(responseList[i].FirstName.Equals(_users[i].FirstName));
            Assert.True(responseList[i].LastName.Equals(_users[i].LastName));
            Assert.True(responseList[i].EmailAddress.Equals(_users[i].EmailAddress));
            Assert.True(responseList[i].UserType.Equals(_users[i].UserType));
            Assert.True(responseList[i].IsActive.Equals(_users[i].IsActive));
            Assert.True(responseList[i].Supplier.SupplierName.Equals(_users[i].Supplier.SupplierName));
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
