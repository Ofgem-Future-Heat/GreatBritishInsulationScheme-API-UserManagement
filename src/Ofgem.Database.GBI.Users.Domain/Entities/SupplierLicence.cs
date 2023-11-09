namespace Ofgem.Database.GBI.Users.Domain.Entities;

public class SupplierLicence
{
    public int SupplierLicenceId { get; set; }
    public string SupplierLicenceReference { get; set; } = string.Empty;
    public string LicenceNo { get; set; } = string.Empty;
    public string LicenceName { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
}