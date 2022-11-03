using Companies.Domain.Base.Models;
using Companies.Domain.Base.Persistence;
using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Models;
using Companies.Domain.Features.Companies.Repositories;

using MediatR;

namespace Companies.Domain.Features.Companies.Handlers;

public class CreateCompanyHandler : IRequestHandler<CreateCompany, Response<Company>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<Company>> Handle(CreateCompany request, CancellationToken cancellationToken)
    {
        if (await _companyRepository.AnyByCnpj(request.Cnpj))
            return Response<Company>.Error(
                new Notification("Company", $"Company with cnpj '{request.Cnpj}' already exists")
            );

        if (await _companyRepository.AnyByName(request.Name))
            return Response<Company>.Error(
                new Notification("Company", $"Company with name '{request.Name}' already exists")
            );

        var company = CreateCompany(request);
        
        await _companyRepository.Add(company);

        await _unitOfWork.CommitAsync();

        return Response<Company>.Ok(company);
    }

    private static Company CreateCompany(CreateCompany request)
    {
        var partners = request.Partners.Select(p =>
            new CompanyPartner(
                partnerId: p.PartnerId,
                qualificationId: p.QualificationId,
                joinedAt: p.JoinedAt
                )
            );

        var phones = request.Phones.Select(p =>
            new CompanyPhone(new Phone(p.CountryCode, p.Number))
            );

        var company = new Company(
            cnpj: new Cnpj(request.Cnpj),
            name: request.Name,
            legalNature: request.LegalNature,
            mainActivityId: request.MainActivityId,
            address: new Address(
                postalCode: request.Address.PostalCode,
                street: request.Address.Street,
                number: request.Address.Number,
                complement: request.Address.Complement,
                neighborhood: request.Address.Neighborhood,
                city: request.Address.City,
                state: request.Address.State,
                country: request.Address.Country
            ),
            partners: partners,
            phones: phones
        );

        return company;
    }
}
