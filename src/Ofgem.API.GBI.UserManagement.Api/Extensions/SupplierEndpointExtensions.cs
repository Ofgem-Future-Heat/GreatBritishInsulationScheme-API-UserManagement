using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;

namespace Ofgem.API.GBI.UserManagement.Api.Extensions
{
    public static class SupplierEndpointExtensions
    {
        public static void MapSupplierEndpoints(this WebApplication app)
        {
            // Get suppliers
            app.MapGet("/suppliers", (ISupplierService supplierService) =>
            {
                IEnumerable<SupplierDto> suppliers = supplierService.GetSuppliers();
                return Results.Ok(suppliers);
            });

            // Get supplier licences 
            app.MapGet("/suppliers-licences", async (ISupplierService SupplierService) =>
            {
                IEnumerable<SupplierLicenceDto> suppliersLicences = await SupplierService.GetSupplierLicencesAsync();
                return Results.Ok(suppliersLicences);
            });

        }
    }
}
