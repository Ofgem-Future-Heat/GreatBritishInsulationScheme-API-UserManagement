using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;
using Ofgem.Database.GBI.Users.Infrastructure;

namespace Ofgem.API.GBI.UserManagement.Infrastructure.Repositories
{
	/// <inheritdoc />
	public class ExternalUserRepository : IExternalUserRepository
	{
		private readonly UsersDbContext _usersDbContext;
		private readonly IMapper _mapper;

		public ExternalUserRepository(UsersDbContext usersDbContext, IMapper mapper)
		{
			_usersDbContext = usersDbContext;
			_mapper = mapper;
		}

		public async Task<Guid> SaveExternalUserAsync(ExternalUser user)
		{
			_usersDbContext.ExternalUsers.Update(user);
			await _usersDbContext.SaveChangesAsync();

			return user.ExternalUserId;
		}

        public async Task<List<ExternalUserDto>> ReadAllExternalUsersAsync()
        {
			return await _usersDbContext.ExternalUsers
				.Include(u => u.Supplier)
				.ProjectTo<ExternalUserDto>(_mapper.ConfigurationProvider)
				.ToListAsync();
        }

		public ExternalUserDto? GetExternalUser(Guid id)
		{
			ExternalUser? externalUser = _usersDbContext.ExternalUsers
				.Include(u => u.Supplier)
				.SingleOrDefault(u => u.ExternalUserId == id);

			return externalUser != null ? _mapper.Map<ExternalUserDto>(externalUser) : null;
		}

        public ExternalUser? GetExternalUserByUniqueId(string uniqueUserId)
        {
			return _usersDbContext.ExternalUsers
				.Include(u => u.Supplier)
				.SingleOrDefault(u => u.UniqueUserId == uniqueUserId);
        }

        public ExternalUser? GetExternalUserByEmail(string email)
        {
			return _usersDbContext.ExternalUsers
				.Include(u => u.Supplier)
				.SingleOrDefault(u => u.EmailAddress == email);
        }

        public async Task DeactivateExternalUser(Guid id)
		{
			var externalUser = new ExternalUser { ExternalUserId = id };

			_usersDbContext.Attach(externalUser);

			externalUser.IsActive = false;

			await _usersDbContext.SaveChangesAsync();
		}
    }
}
