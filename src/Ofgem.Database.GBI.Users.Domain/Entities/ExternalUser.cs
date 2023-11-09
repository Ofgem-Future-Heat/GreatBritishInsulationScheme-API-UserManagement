using Ofgem.Database.GBI.Users.Domain.Enums;

namespace Ofgem.Database.GBI.Users.Domain.Entities
{
    public class ExternalUser
    {
        public Guid ExternalUserId { get; set; }
        public string? UniqueUserId { get; set; }
        public int SupplierId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public ExternalUserType UserType { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Supplier? Supplier { get; set; }
    }
}
