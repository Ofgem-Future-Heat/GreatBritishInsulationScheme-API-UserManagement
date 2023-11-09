using Ofgem.API.GBI.UserManagement.Application.Models;
using Ofgem.Database.GBI.Users.Domain.Dtos;

namespace Ofgem.API.GBI.UserManagement.Application.Interfaces
{
    public interface IExternalUserService
    {
        /// <summary>
		/// Creates or updates a new external user
		/// </summary>
		/// <param name="request">Request</param>
		/// <returns>External user</returns>
		Task<ExternalUserDto> SaveExternalUserAsync(SaveExternalUserRequest request);

        /// <summary>
        /// Retrieve all external users from the db
        /// </summary>
        /// <returns>List of all external users</returns>
        Task<List<ExternalUserDto>> ReadAllExternalUsersAsync();

		/// <summary>
		/// Get external user by Id
		/// </summary>
		/// <param name="id">User Id</param>
		/// <returns>External user</returns>
		ExternalUserDto GetExternalUser(Guid id);

		/// <summary>
		/// Deactivates an external user by Id
		/// </summary>
		/// <param name="id">User Id</param>
		Task DeactivateExternalUser(Guid id);

        /// <summary>
        /// Synchronise identity properties of an external user
        /// </summary>
        /// <param name="uniqueUserId">Unique user Id</param>
        /// <param name="emailAddress">Email Address</param>
        /// <returns>External user</returns>
        Task<ExternalUserDto> SyncExternalUserAsync(string uniqueUserId, string emailAddress);
	}
}
