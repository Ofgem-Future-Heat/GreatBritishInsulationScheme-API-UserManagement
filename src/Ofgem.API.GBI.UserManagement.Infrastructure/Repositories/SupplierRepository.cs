using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;
using Ofgem.Database.GBI.Users.Infrastructure;

namespace Ofgem.API.GBI.UserManagement.Infrastructure.Repositories
{
    /// <inheritdoc />
    public class SupplierRepository : ISupplierRepository
    {
        private readonly UsersDbContext _usersDbContext;
        private readonly IMapper _mapper;

        public SupplierRepository(UsersDbContext usersDbContext, IMapper mapper)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            return _usersDbContext.Suppliers
                .OrderBy(s => s.SupplierId)
                .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public async Task<IEnumerable<SupplierLicenceDto>> GetSupplierLicencesAsync()
        {
            return await _usersDbContext.SupplierLicences
                .ProjectTo<SupplierLicenceDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
