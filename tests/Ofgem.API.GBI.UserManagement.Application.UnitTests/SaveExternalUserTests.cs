using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using Ofgem.API.GBI.UserManagement.Application.Exceptions;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Mappings;
using Ofgem.API.GBI.UserManagement.Application.Models;
using Ofgem.API.GBI.UserManagement.Application.Services;
using Ofgem.Database.GBI.Users.Domain.Entities;
using Xunit;

namespace Ofgem.API.GBI.UserManagement.Application.UnitTests
{
    public class SaveExternalUserTests
    {
        private readonly Mock<IExternalUserRepository> _externalUserRepositoryMock;
        private readonly IMapper _mapper;

        private readonly SaveExternalUserRequest _createExternalUserRequest = new()
        {
            SupplierId = 1,
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "name@example.com"
        };

        public SaveExternalUserTests()
        {
            _externalUserRepositoryMock = new Mock<IExternalUserRepository>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public async void SaveExternalUser_WithNewUser_DoesNotThrowException()
        {
            // Arrange
            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);

            // Act
            var exception = await Record.ExceptionAsync(
                () => externalUserService.SaveExternalUserAsync(_createExternalUserRequest));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void SaveExternalUser_WhenUserExists_ThrowsUserExistsException()
        {
            // Arrange
            _externalUserRepositoryMock.Setup(x => x.SaveExternalUserAsync(It.IsAny<ExternalUser>()))
                .Throws<UniqueConstraintException>();

            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);

            // Act
            var exception = await Record.ExceptionAsync(
                () => externalUserService.SaveExternalUserAsync(_createExternalUserRequest));

            // Assert
            Assert.NotNull(exception);
            Assert.True(exception is UserExistsException);
        }

        [Fact]
        public async void SaveExternalUser_WithNewUserId_ThrowsUserNotFoundException()
        {
            // Arrange
            _externalUserRepositoryMock.Setup(x => x.SaveExternalUserAsync(It.IsAny<ExternalUser>()))
                .Throws<DbUpdateConcurrencyException>();

            var externalUserService = new ExternalUserService(_externalUserRepositoryMock.Object, _mapper);

            _createExternalUserRequest.ExternalUserId = Guid.NewGuid();

            // Act
            var exception = await Record.ExceptionAsync(
                () => externalUserService.SaveExternalUserAsync(_createExternalUserRequest));

            // Assert
            Assert.NotNull(exception);
            Assert.True(exception is UserNotFoundException);
        }
    }
}
