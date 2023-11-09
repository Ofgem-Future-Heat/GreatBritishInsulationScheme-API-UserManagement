using Microsoft.EntityFrameworkCore;

namespace Ofgem.Database.GBI.Users.Infrastructure.UnitTests
{
    public class UsersDbContextUnitTests
    {
        DbContextOptions<UsersDbContext> _options;

        public UsersDbContextUnitTests()
        {
            _options = new DbContextOptionsBuilder<UsersDbContext>()
                 .UseInMemoryDatabase(databaseName: "UsersDatabase")
                 .Options;
        }


        [Fact]
        public void UsersDbContextGetTypeTest()
        {
            var dbContext = new UsersDbContext(_options);
            Assert.True(dbContext.GetType().Name == nameof(UsersDbContext));
        }
    }
}