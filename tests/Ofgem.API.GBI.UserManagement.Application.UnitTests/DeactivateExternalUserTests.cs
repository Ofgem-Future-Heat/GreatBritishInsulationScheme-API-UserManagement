using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Exceptions;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Mappings;
using Ofgem.API.GBI.UserManagement.Application.Services;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Application.UnitTests
{
    public class DeactivateExternalUserTests
    {
        private readonly Mock<IExternalUserRepository> _externalUserRepositoryMock;
        private readonly IMapper _mapper;

        public DeactivateExternalUserTests()
        {
            _externalUserRepositoryMock = new Mock<IExternalUserRepository>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public async void DeactivateExternalUser_WhenUserExists_DoesNotThrowException()
        {
            // Arrange
            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);

            // Act
            var exception = await Record.ExceptionAsync(
                () => externalUserService.DeactivateExternalUser(Guid.NewGuid()));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void DeactivateExternalUser_WhenUserDoesNotExist_ThrowsException()
        {
            // Arrange
            _externalUserRepositoryMock.Setup(x => x.DeactivateExternalUser(It.IsAny<Guid>()))
                .Throws<DbUpdateConcurrencyException>();

            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);

            // Act
            var exception = await Record.ExceptionAsync(
                () => externalUserService.DeactivateExternalUser(Guid.NewGuid()));

            // Assert
            Assert.NotNull(exception);
            Assert.True(exception is UserNotFoundException);
        }
    }
}
