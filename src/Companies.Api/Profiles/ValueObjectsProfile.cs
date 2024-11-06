using AutoMapper;

using Companies.Application.Base.Models;
using Companies.Application.Base.ValueObjects;

namespace Companies.Api.Profiles;

public class ValueObjectsProfile : Profile
{
    public ValueObjectsProfile()
    {
        CreateMap<Address, AddressModel>();
        CreateMap<Phone, PhoneModel>();

    }
}
