namespace Ofgem.Database.GBI.Users.Domain.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public ICollection<SupplierLicence>? SupplierLicences { get; set; }
        public ICollection<ExternalUser>? ExternalUsers { get; set; }
    }
}
