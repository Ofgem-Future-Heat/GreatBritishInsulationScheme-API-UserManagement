using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Ofgem.Database.GBI.Users.Domain.Entities;

namespace Ofgem.Database.GBI.Users.Infrastructure
{
    public class UsersDbContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierLicence> SupplierLicences { get; set; }
        public DbSet<ExternalUser> ExternalUsers { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);

            modelBuilder.Entity<Supplier>(supplier =>
            {
                supplier.HasKey(e => e.SupplierId);
                supplier.HasIndex(e => e.SupplierName).IsUnique();
            });

            modelBuilder.Entity<SupplierLicence>(supplierLicence =>
            {
                supplierLicence.HasKey(e => e.SupplierLicenceId);
                supplierLicence.HasIndex(e => e.SupplierLicenceReference).IsUnique();
                supplierLicence.Property(e => e.SupplierLicenceReference).HasMaxLength(20);
                supplierLicence.Property(e => e.LicenceNo).HasMaxLength(20);
                supplierLicence.Property(e => e.LicenceName).HasMaxLength(255);
            });

            modelBuilder.Entity<ExternalUser>(user =>
            {
                user.HasKey(e => e.ExternalUserId);
				user.HasIndex(e => e.UniqueUserId).IsUnique();
				user.HasIndex(e => e.EmailAddress).IsUnique();
            });
        }
    }
}
