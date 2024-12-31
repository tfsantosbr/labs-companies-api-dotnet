using Companies.Application.Abstractions.Handlers;
using Companies.Application.Features.Companies.Models;

namespace Companies.Application.Features.Companies.Commands.AddPartnerInCompany;

public record AddPartnerInCompanyCommand(Guid CompanyId, Guid PartnerId, int QualificationId, DateTime JoinedAt)
    : ICommand<CompanyPartnerModel>;
