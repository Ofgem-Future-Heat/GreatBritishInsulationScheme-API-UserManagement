using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Ofgem.API.GBI.UserManagement.Application.Exceptions;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Models;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;

namespace Ofgem.API.GBI.UserManagement.Application.Services
{
	/// <inheritdoc />
	public class ExternalUserService : IExternalUserService
	{
		private readonly IExternalUserRepository _externalUserRepository;
		private readonly IMapper _mapper;

		public ExternalUserService(IExternalUserRepository externalUserRepository, IMapper mapper)
		{
			_externalUserRepository = externalUserRepository;
			_mapper = mapper;
		}

		public async Task<ExternalUserDto> SaveExternalUserAsync(SaveExternalUserRequest request)
		{
			var user = _mapper.Map<ExternalUser>(request);

			try
			{
				await _externalUserRepository.SaveExternalUserAsync(user);
			}
			catch (UniqueConstraintException)
			{
				throw new UserExistsException();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (request.ExternalUserId != Guid.Empty)
				{
					EnsureExternalUserExists(request.ExternalUserId);
				}

				throw;
			}

			return _mapper.Map<ExternalUserDto>(user);
		}

        public async Task<List<ExternalUserDto>> ReadAllExternalUsersAsync()
        {
			return await _externalUserRepository.ReadAllExternalUsersAsync();
        }

		public ExternalUserDto GetExternalUser(Guid id)
		{
			ExternalUserDto? user = _externalUserRepository.GetExternalUser(id);

			return user ?? throw new UserNotFoundException();
		}

        public async Task DeactivateExternalUser(Guid id)
		{
			try
			{
				await _externalUserRepository.DeactivateExternalUser(id);
			}
			catch (DbUpdateConcurrencyException)
			{
				EnsureExternalUserExists(id);
				throw;
			}
		}

        public async Task<ExternalUserDto> SyncExternalUserAsync(string uniqueUserId, string emailAddress)
        {
			var user = _externalUserRepository.GetExternalUserByUniqueId(uniqueUserId);

            if (user == null)
            {
				user = await SetUniqueUserIdAsync(uniqueUserId, emailAddress);
            }
            else
            {
				await UpdateEmailAddress(user, emailAddress);
            }

			return _mapper.Map<ExternalUserDto>(user);
        }

		/// <summary>
		/// Sets unique user Id on external user by the given email address
		/// </summary>
		/// <param name="uniqueUserId">Unique user Id</param>
		/// <param name="emailAddress">Email address</param>
		/// <returns>External user</returns>
		/// <exception cref="UserNotFoundException"></exception>
		private async Task<ExternalUser> SetUniqueUserIdAsync(string uniqueUserId, string emailAddress)
		{
            ExternalUser user = _externalUserRepository.GetExternalUserByEmail(emailAddress) ?? throw new UserNotFoundException();

            user.UniqueUserId = uniqueUserId;
            await _externalUserRepository.SaveExternalUserAsync(user);

			return user;
        }

		/// <summary>
		/// Updates the email address of an external user if the email address has changed
		/// </summary>
		/// <param name="user">External user</param>
		/// <param name="emailAddress">Email address</param>
		private async Task UpdateEmailAddress(ExternalUser user, string emailAddress)
		{
			if (user.EmailAddress != emailAddress)
			{
                user.EmailAddress = emailAddress;
                await _externalUserRepository.SaveExternalUserAsync(user);
            }
		}

        /// <summary>
        /// Checks if a user with the given Id exists in the database.
        /// An exception is thrown if the user does not exist.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <exception cref="UserNotFoundException">Exception</exception>
        private void EnsureExternalUserExists(Guid id)
		{
			ExternalUserDto? existingUser = _externalUserRepository.GetExternalUser(id);

			// Throw new exception if user does not exist
			if (existingUser == null)
			{
				throw new UserNotFoundException();
			}
		}
    }
}
