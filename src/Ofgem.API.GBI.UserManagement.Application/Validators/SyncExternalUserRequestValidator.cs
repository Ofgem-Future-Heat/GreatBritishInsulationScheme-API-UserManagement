using FluentValidation;
using Ofgem.API.GBI.UserManagement.Application.Models;

namespace Ofgem.API.GBI.UserManagement.Application.Validators
{
    public class SyncExternalUserRequestValidator : AbstractValidator<SyncExternalUserRequest>
    {
        public SyncExternalUserRequestValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress().MaximumLength(250);
        }
    }
}
