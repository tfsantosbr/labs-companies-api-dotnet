using Companies.Domain.Base.Models;
using Companies.Domain.Base.Persistence;
using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Commands.Validators;
using Companies.Domain.Features.Companies.Repositories;

using MediatR;

namespace Companies.Domain.Features.Companies.Handlers;

public class UpdateCompanyHandler : IRequestHandler<UpdateCompany, Response>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public UpdateCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response> Handle(UpdateCompany request, CancellationToken cancellationToken)
    {
        if (IsInvalidRequest(request, out var notifications))
            return RequestErrorsResponse(notifications);

        if (await IsDuplicatedName(request.Name, request.CompanyId))
            return ErrorResponse("Company", $"Company with name '{request.Name}' already exists");

        var company = await _companyRepository.GetById(request.CompanyId);

        if (company == null)
            return ErrorResponse("Company", "Company not found");

        UpdateCompanyFromRequest(request, company);

        await _unitOfWork.CommitAsync();

        return Response<Company>.Ok(company);
    }

    // Private Methods

    private static void UpdateCompanyFromRequest(UpdateCompany request, Company company)
    {
        var phones = request.Phones.Select(p =>
            new CompanyPhone(new Phone(p.CountryCode, p.Number))
        );

        company.Update(
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
            phones: phones
        );
    }

    private bool IsInvalidRequest(UpdateCompany request, out IEnumerable<Notification> notifications)
    {
        var validator = new UpdateCompanyValidator();
        var result = validator.Validate(request);

        notifications = result.Errors.Select(e =>
            new Notification(e.PropertyName, e.ErrorMessage)
        );

        return !result.IsValid;
    }

    private async Task<bool> IsDuplicatedName(string name, Guid ignoreId)
    {
        return await _companyRepository.AnyByName(name, ignoreId);
    }

    private Response ErrorResponse(string code, string errorMessage)
    {
        return Response.Error(new Notification(code, errorMessage));
    }

    private Response RequestErrorsResponse(IEnumerable<Notification> notifications)
    {
        return Response.Error(notifications);
    }
}
