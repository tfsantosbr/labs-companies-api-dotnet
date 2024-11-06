using AutoMapper;

using Companies.Application.Abstractions.Models;
using Companies.Application.Abstractions.ValueObjects;

namespace Companies.Api.Profiles;

public class ValueObjectsProfile : Profile
{
    public ValueObjectsProfile()
    {
        CreateMap<Address, AddressModel>();
        CreateMap<Phone, PhoneModel>();

    }
}
