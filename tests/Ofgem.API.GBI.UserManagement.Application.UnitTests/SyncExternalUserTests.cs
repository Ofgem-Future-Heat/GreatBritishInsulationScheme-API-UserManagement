using AutoMapper;
using EntityFramework.Exceptions.Common;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Exceptions;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Mappings;
using Ofgem.API.GBI.UserManagement.Application.Services;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Application.UnitTests
{
    public class SyncExternalUserTests
    {
        private readonly Mock<IExternalUserRepository> _externalUserRepositoryMock;
        private readonly IMapper _mapper;

        public SyncExternalUserTests()
        {
            _externalUserRepositoryMock = new Mock<IExternalUserRepository>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public async void SyncExternalUser_WithNewProviderId_ReturnsUser()
        {
            // Arrange
            _externalUserRepositoryMock.Setup(x => x.GetExternalUserByEmail(It.IsAny<string>())).Returns(new ExternalUser());

            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);
            string providerId = Guid.NewGuid().ToString();

            // Act
            ExternalUserDto user = await externalUserService.SyncExternalUserAsync(providerId, "name@example.com");

            // Assert
            _externalUserRepositoryMock.Verify(x => x.SaveExternalUserAsync(It.IsAny<ExternalUser>()));
            Assert.NotNull(user);
            Assert.Equal(providerId, user.UniqueUserId);
        }

        [Fact]
        public async void SyncExternalUser_WithExistingProviderId_UpdatesEmailAddress()
        {
            // Arrange
            var externalUser = new ExternalUser
            {
                UniqueUserId = Guid.NewGuid().ToString(),
                EmailAddress = "name@example.com"
            };

            _externalUserRepositoryMock.Setup(x => x.GetExternalUserByUniqueId(It.IsAny<string>())).Returns(externalUser);

            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);

            // Act
            ExternalUserDto user = await externalUserService.SyncExternalUserAsync(externalUser.UniqueUserId, "name2@example.com");

            // Assert
            _externalUserRepositoryMock.Verify(x => x.SaveExternalUserAsync(It.IsAny<ExternalUser>()));
            Assert.NotNull(user);
            Assert.Equal(externalUser.EmailAddress, user.EmailAddress);
        }

        [Fact]
        public async void SyncExternalUser_WithInvalidEmail_ThrowsException()
        {
            // Arrange
            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);
            string providerId = Guid.NewGuid().ToString();

            // Act
            var exception = await Record.ExceptionAsync(() => externalUserService.SyncExternalUserAsync(providerId, "name@example.com"));

            // Assert
            Assert.NotNull(exception);
            Assert.True(exception is UserNotFoundException);
        }
    }
}
