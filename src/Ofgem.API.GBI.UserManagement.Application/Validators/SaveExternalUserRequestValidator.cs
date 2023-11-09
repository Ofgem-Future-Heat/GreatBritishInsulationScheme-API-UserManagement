using FluentValidation;
using Ofgem.API.GBI.UserManagement.Application.Models;

namespace Ofgem.API.GBI.UserManagement.Application.Validators
{
	public class SaveExternalUserRequestValidator : AbstractValidator<SaveExternalUserRequest>
	{
		public SaveExternalUserRequestValidator()
		{
			RuleFor(x => x.SupplierId).NotEmpty();
			RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
			RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
			RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress().MaximumLength(250);
			RuleFor(x => x.UserType).NotNull();
		}
	}
}
