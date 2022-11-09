using AutoMapper;

using Companies.Domain.Base.Models;
using Companies.Domain.Base.ValueObjects;

namespace Companies.Api.Profiles;

public class ValueObjectsProfile : Profile
{
    public ValueObjectsProfile()
    {
        CreateMap<Address, AddressModel>();
        CreateMap<Phone, PhoneModel>();
        
    }
}
