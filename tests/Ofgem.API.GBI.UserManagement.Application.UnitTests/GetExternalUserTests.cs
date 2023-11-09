using AutoMapper;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Exceptions;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Mappings;
using Ofgem.API.GBI.UserManagement.Application.Services;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Application.UnitTests
{
    public class GetExternalUserTests
    {
        private readonly Mock<IExternalUserRepository> _externalUserRepositoryMock;
        private readonly IMapper _mapper;

        public GetExternalUserTests()
        {
            _externalUserRepositoryMock = new Mock<IExternalUserRepository>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public void GetExternalUser_WhenUserExits_ReturnsUser()
        {
            // Arrange
            _externalUserRepositoryMock.Setup(x => x.GetExternalUser(It.IsAny<Guid>())).Returns(new ExternalUserDto());
            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);

            // Act
            ExternalUserDto user = externalUserService.GetExternalUser(Guid.NewGuid());

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void GetExternalUser_WhenUserDoesNotExit_ThrowsException()
        {
            // Arrange
            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);

            // Act
            var exception = Record.Exception(() => externalUserService.GetExternalUser(Guid.NewGuid()));

            // Assert
            Assert.NotNull(exception);
            Assert.True(exception is UserNotFoundException);
        }
    }
}
