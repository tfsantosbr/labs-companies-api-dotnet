using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.ValueObjects;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Features.CompanyMainActivities;

using FluentValidation;

namespace Companies.Application.Features.Companies;

public class Company
{
    // Fields

    private readonly List<CompanyPartner> _partners = [];
    private readonly List<CompanyPhone> _phones = [];

    // Constructors

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
    public IReadOnlyCollection<CompanyPartner> Partners => _partners.AsReadOnly();
    public IReadOnlyCollection<CompanyPhone> Phones => _phones.AsReadOnly();

    // Public Methods

    public static Result<Company> Create(
        Cnpj cnpj, string name, CompanyLegalNatureType legalNature, int mainActivityId,
        Address address, IEnumerable<CompanyPartner> partners, IEnumerable<CompanyPhone>? phones = null,
        Guid? id = null)
    {
        if (HasEmptyPartners(partners))
            return Result<Company>.Error(CompanyErrors.CompanyMustBeCreatedWithAtLeastOnePartner());

        if (IsDuplicatedPartners(partners))
            return Result<Company>.Error(CompanyErrors.CompanyCannotHaveDuplicatedPartners());

        if (IsDuplicatedPhones(phones))
            return Result<Company>.Error(CompanyErrors.CompanyCanotHaveDuplicatedPhones());

        var company = new Company
        {
            Id = id ?? Guid.NewGuid(),
            Cnpj = cnpj,
            Name = name,
            LegalNature = legalNature,
            MainActivityId = mainActivityId,
            Address = address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        company.AddPhones(phones);

        foreach (var partner in partners)
            company.AddPartner(partner);

        return Result<Company>.Success(company);
    }

    public Result Update(string name, CompanyLegalNatureType legalNature, int mainActivityId,
        Address address, IEnumerable<CompanyPhone> phones)
    {
        Name = name;
        LegalNature = legalNature;
        MainActivityId = mainActivityId;
        Address = address;
        UpdatedAt = DateTime.UtcNow;

        if (IsDuplicatedPhones(phones))
            return Result.Error(CompanyErrors.CompanyCanotHaveDuplicatedPhones());

        UpdatePhones(phones);

        return Result.Success();
    }

    public Result AddPartner(CompanyPartner partner)
    {
        if (IsDuplicatedPartner(partner))
            return Result.Error(CompanyErrors.PartnerAlreadyLinkedWithCompany());

        _partners.Add(partner);

        return Result.Success();
    }

    public Result RemovePartner(CompanyPartner partner)
    {
        if (PartnerNotExists(partner))
            return Result.Error(CompanyErrors.CompanysPartnerNotExists());

        _partners.Remove(partner);

        return Result.Success();
    }

    // Private Methods

    private static bool IsDuplicatedPartners(IEnumerable<CompanyPartner> partners) => partners
            .GroupBy(p => p.PartnerId)
            .Any(g => g.Count() > 1);

    private static bool HasEmptyPartners(IEnumerable<CompanyPartner> partners) => !partners.Any();

    private bool IsDuplicatedPartner(CompanyPartner partner) => _partners.Any(p => p.PartnerId == partner.PartnerId);

    private bool PartnerNotExists(CompanyPartner partner) => !_partners.Any(p => p.PartnerId == partner.PartnerId);

    private void UpdatePhones(IEnumerable<CompanyPhone>? phones)
    {
        _phones.Clear();

        if (phones == null || !phones.Any())
            return;

        foreach (var phone in phones)
            _phones.Add(phone);
    }

    private void AddPhones(IEnumerable<CompanyPhone>? phones)
    {
        if (phones == null || !phones.Any())
            return;

        _phones.Clear();

        foreach (var phone in phones)
            _phones.Add(phone);
    }

    private static bool IsDuplicatedPhones(IEnumerable<CompanyPhone>? phones) => phones != null && phones
            .GroupBy(p => new { p.Phone.CountryCode, p.Phone.Number })
            .Any(g => g.Count() > 1);
}
