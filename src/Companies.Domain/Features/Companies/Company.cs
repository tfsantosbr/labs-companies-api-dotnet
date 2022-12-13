using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.Companies.Enums;
using Companies.Domain.Features.CompanyMainActivities;

using FluentValidation;

namespace Companies.Domain.Features.Companies;

public class Company
{
    // Fields

    private readonly List<CompanyPartner> _partners = new List<CompanyPartner>();
    private readonly List<CompanyPhone> _phones = new List<CompanyPhone>();

    // Constructors

    public Company(
        Cnpj cnpj, string name, CompanyLegalNatureType legalNature, int mainActivityId,
        Address address, IEnumerable<CompanyPartner> partners, IEnumerable<CompanyPhone>? phones = null,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Cnpj = cnpj;
        Name = name;
        LegalNature = legalNature;
        MainActivityId = mainActivityId;
        Address = address;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        if (HasEmptyPartners(partners))
            throw new Exception("The company must be created with at least one partner");

        if (IsDuplicatedPartners(partners))
            throw new Exception("There are duplicate partners in the company");

        if (IsDuplicatedPhones(phones))
            throw new Exception("There are duplicate phones in the company");

        AddPhones(phones);

        foreach (var partner in partners)
            AddPartner(partner);
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
    public IReadOnlyCollection<CompanyPartner> Partners => _partners.AsReadOnly();
    public IReadOnlyCollection<CompanyPhone> Phones => _phones.AsReadOnly();

    // Public Methods

    public void Update(
        string name, CompanyLegalNatureType legalNature, int mainActivityId,
        Address address, IEnumerable<CompanyPhone> phones)
    {
        Name = name;
        LegalNature = legalNature;
        MainActivityId = mainActivityId;
        Address = address;
        UpdatedAt = DateTime.UtcNow;

        if (IsDuplicatedPhones(phones))
            throw new Exception("There are duplicate phones in the company");

        UpdatePhones(phones);
    }

    public void AddPartner(CompanyPartner partner)
    {
        if (IsDuplicatedPartner(partner))
            throw new Exception("This partner is already linked with this company");

        _partners.Add(partner);
    }

    
    public CompanyPartner? GetPartner(Guid partnerId)
    {
        return _partners.FirstOrDefault(p=>p.PartnerId==partnerId);
    }

    public void RemovePartner(CompanyPartner partner)
    {
        if (PartnerNotExists(partner))
            throw new Exception("This partner not exists in this company");

        _partners.Remove(partner);
    }


    // Private Methods

    private bool IsDuplicatedPartners(IEnumerable<CompanyPartner> partners)
    {
        return partners
            .GroupBy(p => p.PartnerId)
            .Any(g => g.Count() > 1);
    }

    private bool HasEmptyPartners(IEnumerable<CompanyPartner> partners)
    {
        return !partners.Any();
    }

    private bool IsDuplicatedPartner(CompanyPartner partner)
    {
        return _partners.Any(p => p.PartnerId == partner.PartnerId);
    }


    private bool PartnerNotExists(CompanyPartner partner)
    {
        return !_partners.Any(p => p.PartnerId == partner.PartnerId);
    }

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


    private bool IsDuplicatedPhones(IEnumerable<CompanyPhone>? phones)
    {
        if (phones == null)
            return false;

        return phones
            .GroupBy(p => new { p.Phone.CountryCode, p.Phone.Number })
            .Any(g => g.Count() > 1);
    }
}
