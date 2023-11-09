using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Models;
using Ofgem.Database.GBI.Users.Domain.Dtos;

namespace Ofgem.API.GBI.UserManagement.Api.Extensions
{
    public static class ExternalUserEndpointExtensions
    {
        public static void MapExternalUserEndpoints(this WebApplication app)
        {
            // Create external user
            app.MapPut("/external-users", async (
                [FromBody] SaveExternalUserRequest request,
                IValidator<SaveExternalUserRequest> validator,
                IExternalUserService externalUserService) =>
            {
                ValidationResult validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                ExternalUserDto user = await externalUserService.SaveExternalUserAsync(request);

                return Results.Ok(new
                {
					user.ExternalUserId
                });
            });

            // Read all extrenal users
            app.MapGet("/external-users", async (IExternalUserService externalUserService) =>
            {
                return Results.Ok(await externalUserService.ReadAllExternalUsersAsync());
            });

            // Get external user by Id
            app.MapGet("/external-users/{id}", (Guid id, IExternalUserService externalUserService) =>
            {
                ExternalUserDto user = externalUserService.GetExternalUser(id);
                return Results.Ok(user);
            });

            // Deactivate external user by Id
            app.MapPatch("/external-users/{id}/deactivate", async (Guid id, IExternalUserService externalUserService) =>
            {
                await externalUserService.DeactivateExternalUser(id);
                return Results.NoContent();
            });

            // Set unique user Id and email address on external user
            app.MapPut("/external-users/{uniqueUserId}/sync", async (
                string uniqueUserId,
                [FromBody] SyncExternalUserRequest request,
                IValidator<SyncExternalUserRequest> validator,
                IExternalUserService externalUserService) =>
            {
                ValidationResult validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                ExternalUserDto user = await externalUserService.SyncExternalUserAsync(uniqueUserId, request.EmailAddress);
                return Results.Ok(user);
            });
        }
    }
}
