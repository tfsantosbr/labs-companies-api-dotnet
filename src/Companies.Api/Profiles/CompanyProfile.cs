using AutoMapper;

using Companies.Application.Base.Models;
using Companies.Application.Base.ValueObjects;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Models;

namespace Companies.Api.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDetails>();
        CreateMap<CompanyPartner, CompanyPartnerModel>()
            .ForMember(d => d.JoinedAt, o => o.MapFrom(s => s.JoinedAt.ToDateTime(TimeOnly.MinValue)))
            .ForMember(d => d.QualificationId, o => o.MapFrom(s => s.QualificationId));
        CreateMap<CompanyPhone, CompanyPhoneModel>();
    }
}
