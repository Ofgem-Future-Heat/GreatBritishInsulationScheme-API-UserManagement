using Ofgem.Database.GBI.Users.Domain.Enums;

namespace Ofgem.API.GBI.UserManagement.Application.Models
{
    public class SaveExternalUserRequest
    {
        public Guid ExternalUserId { get; set; }
        public int SupplierId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public ExternalUserType UserType { get; set; }
    }
}
