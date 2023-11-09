using AutoMapper;
using Ofgem.API.GBI.UserManagement.Application.Models;
using Ofgem.Database.GBI.Users.Domain.Dtos;
using Ofgem.Database.GBI.Users.Domain.Entities;

namespace Ofgem.API.GBI.UserManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaveExternalUserRequest, ExternalUser>();
            CreateMap<ExternalUser, ExternalUserDto>();
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierLicence, SupplierLicenceDto>()
                .ForMember(dest => dest.SupplierName, opts => opts.MapFrom(src => src.Supplier.SupplierName));
        }
    }
}
