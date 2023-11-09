using Ofgem.Database.GBI.Users.Domain.Enums;

namespace Ofgem.Database.GBI.Users.Domain.Dtos
{
    public class ExternalUserDto
    {
        public Guid ExternalUserId { get; set; }
		public string? UniqueUserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public ExternalUserType UserType { get; set; }
        public bool IsActive { get; set; }
        public SupplierDto? Supplier { get; set; }
    }
}
