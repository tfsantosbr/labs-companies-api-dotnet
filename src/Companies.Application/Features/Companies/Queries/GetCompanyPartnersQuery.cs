﻿using Companies.Application.Abstractions.Handlers;
using Companies.Application.Features.Companies.Models;

namespace Companies.Application.Features.Companies.Queries;

public record GetCompanyPartnersQuery(Guid CompanyId) : IQuery<IEnumerable<CompanyPartnerModel>>;
