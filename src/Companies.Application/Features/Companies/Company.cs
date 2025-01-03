using Companies.Application.Abstractions.Domains;
using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.ValueObjects;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Features.Companies.Events;
using Companies.Application.Features.CompanyMainActivities;

namespace Companies.Application.Features.Companies;

public class Company : AggregateRoot
{
    // Fields

    private readonly List<CompanyPartner> _partners = [];
    private readonly List<CompanyPhone> _phones = [];

    // Constructors

    private Company(
        Cnpj cnpj, string name, CompanyLegalNatureType legalNature,
        int mainActivityId, Address address, Guid id)
    {
        Id = id;
        Cnpj = cnpj;
        Name = name;
        LegalNature = legalNature;
        MainActivityId = mainActivityId;
        Address = address;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        RaiseEvent(new CompanyCreatedDomainEvent(Id, Name));
    }

    private Company()
    {
    }

    // Properties

    public Guid Id { get; private set; }
    public Cnpj Cnpj { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public CompanyLegalNatureType LegalNature { get; private set; }
    public int MainActivityId { get; private set; }
    public Address Address { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public CompanyMainActivity MainActivity { get; private set; } = default!;
    public bool IsOperational => IsCompanyOperational();
    public IReadOnlyCollection<CompanyPartner> Partners => _partners.AsReadOnly();
    public IReadOnlyCollection<CompanyPhone> Phones => _phones.AsReadOnly();

    // Public Methods

    public static Result<Company> Create(
        Cnpj cnpj, string name, CompanyLegalNatureType legalNature,
        int mainActivityId, Address address, Guid? id = null)
    {
        var company = new Company(
            id: id ?? Guid.NewGuid(),
            cnpj: cnpj,
            name: name,
            legalNature: legalNature,
            mainActivityId: mainActivityId,
            address: address
            );

        return Result<Company>.Success(company);
    }

    public Result Update(string name, CompanyLegalNatureType legalNature, int mainActivityId, Address address)
    {
        Name = name;
        LegalNature = legalNature;
        MainActivityId = mainActivityId;
        Address = address;
        UpdatedAt = DateTime.UtcNow;

        RaiseEvent(new CompanyUpdatedDomainEvent(Id, Name));

        return Result.Success();
    }

    public Result<CompanyPartner> AddPartner(Guid partnerId, int qualificationId, DateOnly joinedAt)
    {
        if (IsDuplicatedPartner(partnerId))
            return Result<CompanyPartner>.Error(CompanyErrors.PartnerAlreadyLinkedWithCompany());

        var partner = new CompanyPartner(Id, partnerId, qualificationId, joinedAt);

        _partners.Add(partner);

        RaiseEvent(new CompanyPartnerAddedDomainEvent(Id, partnerId));

        return Result<CompanyPartner>.Success(partner);
    }

    public Result RemovePartner(Guid partnerId)
    {
        var partner = _partners.FirstOrDefault(p => p.PartnerId == partnerId);

        if (partner is null)
            return Result.Error(CompanyErrors.CompanysPartnerNotExists());

        _partners.Remove(partner);

        RaiseEvent(new CompanyPartnerRemovedDomainEvent(Id, partnerId));

        return Result.Success();
    }

    public Result<CompanyPhone> AddPhone(Phone phone, Guid? id = null)
    {
        if (IsDuplicatedPhone(phone))
            return Result<CompanyPhone>.Error(CompanyErrors.CompanyPhoneAlreadyExists(phone));

        var companyPhone = new CompanyPhone(Id, phone, id ?? Guid.NewGuid());

        _phones.Add(companyPhone);

        return Result<CompanyPhone>.Success(companyPhone);
    }

    public void ClearPhones()
    {
        _phones.Clear();
    }

    public void Remove()
    {
        RaiseEvent(new CompanyRemovedDomainEvent(Id, Name));
    }

    // Private Methods

    private bool IsDuplicatedPartner(Guid partnerId) => _partners.Any(p => p.PartnerId == partnerId);

    private bool IsDuplicatedPhone(Phone phone) => _phones.Any(p => p.Phone == phone);

    private bool IsCompanyOperational()
    {
        bool hasAtLeatOnePartner = _partners.Count > 0;
        bool hasAtLeatOnePhone = _phones.Count > 0;

        return hasAtLeatOnePartner && hasAtLeatOnePhone;
    }
}