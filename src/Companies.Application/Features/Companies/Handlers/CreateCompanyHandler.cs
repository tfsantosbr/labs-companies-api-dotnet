using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Models;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Results;
using Companies.Application.Base.ValueObjects;
using Companies.Application.Features.Companies.Commands;
using Companies.Application.Features.Companies.Commands.Validators;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Handlers;

public class CreateCompanyHandler : CommandHandler<Company>, IHandler<CreateCompany, Response<Company>>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public CreateCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response<Company>> Handle(CreateCompany request, CancellationToken cancellationToken)
    {
        if (IsInvalidRequest(request, out var notifications))
            return RequestErrorsResponse(notifications);

        if (await IsDuplicatedCnpj(request.Cnpj))
            return ErrorResponse("Company", $"Company with cnpj '{request.Cnpj}' already exists");

        if (await IsDuplicatedName(request.Name))
            return ErrorResponse("Company", $"Company with name '{request.Name}' already exists");

        var company = CreateCompanyFromRequest(request);

        await _companyRepository.Add(company);

        await _unitOfWork.CommitAsync();

        return Response<Company>.Ok(company);
    }

    // Private Methods

    private bool IsInvalidRequest(CreateCompany request, out IEnumerable<Notification> notifications)
    {
        var validator = new CreateCompanyValidator();
        var result = validator.Validate(request);

        notifications = result.Errors.Select(e =>
            new Notification(e.PropertyName, e.ErrorMessage)
        );

        return !result.IsValid;
    }

    private static Company CreateCompanyFromRequest(CreateCompany request)
    {
        var partners = request.Partners.Select(p =>
            new CompanyPartner(
                partnerId: p.PartnerId,
                qualificationId: p.QualificationId,
                joinedAt: DateOnly.FromDateTime(p.JoinedAt)
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

    private async Task<bool> IsDuplicatedName(string name)
    {
        return await _companyRepository.AnyByName(name);
    }

    private async Task<bool> IsDuplicatedCnpj(string cnpj)
    {
        return await _companyRepository.AnyByCnpj(cnpj);
    }


}
