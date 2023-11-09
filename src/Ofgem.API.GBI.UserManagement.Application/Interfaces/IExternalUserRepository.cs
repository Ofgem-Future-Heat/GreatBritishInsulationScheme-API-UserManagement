using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;

namespace Ofgem.API.GBI.UserManagement.Application.Interfaces
{
	public interface IExternalUserRepository
	{
		/// <summary>
		/// Creates or updates external user
		/// </summary>
		/// <param name="user">User</param>
		/// <returns>User Id</returns>
		Task<Guid> SaveExternalUserAsync(ExternalUser user);

		/// <summary>
		/// Retrieves all external users from the db
		/// </summary>
		/// <returns>List of all external users</returns>
		Task<List<ExternalUserDto>> ReadAllExternalUsersAsync();

		/// <summary>
		/// Get external user by Id
		/// </summary>
		/// <param name="id">User Id</param>
		/// <returns>External user</returns>
		ExternalUserDto? GetExternalUser(Guid id);

		/// <summary>
		/// Get external user by provider Id
		/// </summary>
		/// <param name="providerId">Provider Id</param>
		/// <returns>External user</returns>
		ExternalUser? GetExternalUserByUniqueId(string providerId);

		/// <summary>
		/// Get external user by email address
		/// </summary>
		/// <param name="email">Email address</param>
		/// <returns>External user</returns>
		ExternalUser? GetExternalUserByEmail(string email);

		/// <summary>
		/// Deactivates external user by Id
		/// </summary>
		/// <param name="id">User Id</param>
		Task DeactivateExternalUser(Guid id);
	}
}
