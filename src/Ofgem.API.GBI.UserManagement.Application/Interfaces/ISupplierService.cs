using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;

namespace Ofgem.API.GBI.UserManagement.Application.Interfaces
{
    public interface ISupplierService
    {
        /// <summary>
        /// Returns a list of all suppliers
        /// </summary>
        /// <returns>List of suppliers</returns>
        IEnumerable<SupplierDto> GetSuppliers();

        /// <summary>
        /// Returns a list of all supplier licences
        /// </summary>
        /// <returns> List of supplier licences </returns>
        Task<IEnumerable<SupplierLicenceDto>> GetSupplierLicencesAsync();

    }
}
