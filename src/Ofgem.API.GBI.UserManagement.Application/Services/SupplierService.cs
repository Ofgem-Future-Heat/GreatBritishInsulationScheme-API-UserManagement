using Microsoft.Extensions.Logging;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;

namespace Ofgem.API.GBI.UserManagement.Application.Services
{
    /// <inheritdoc />
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(ISupplierRepository supplierRepository, ILogger<SupplierService> logger)
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            try
            {
                return _supplierRepository.GetSuppliers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve suppliers from database. Error: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierLicenceDto>> GetSupplierLicencesAsync()
        {
            try
            {
                return await _supplierRepository.GetSupplierLicencesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve supplier licences from database. Error: {Message}", ex.Message);
                throw;
            }
        }
    }
}
