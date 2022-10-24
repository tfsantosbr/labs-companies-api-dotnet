using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.Companies.Enums;
using Companies.Domain.Features.CompanyMainActivities;

namespace Companies.Domain.Features.Companies;

public class Company
{
    // Fields

    private readonly List<CompanyPartner> _partners = new List<CompanyPartner>();
    private readonly List<CompanyPhone> _phones = new List<CompanyPhone>();

    // Constructors

    public Company(
        Cnpj cnpj, string name, CompanyLegalNatureType legalNature, int mainActivityId,
        Address address, DateTime createdAt, DateTime updatedAt, IEnumerable<CompanyPartner> partners, 
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Cnpj = cnpj;
        Name = name;
        LegalNature = legalNature;
        MainActivityId = mainActivityId;
        Address = address;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;

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
        Address address, DateTime createdAt, DateTime updatedAt, IEnumerable<CompanyPhone> phones)
    {
        Name = name;
        LegalNature = legalNature;
        MainActivityId = mainActivityId;
        Address = address;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        
        UpdatePhones(phones);
    }

    public void AddPartner(CompanyPartner partner) => _partners.Add(partner);
    public void RemovePartner(CompanyPartner partner) => _partners.Remove(partner);

    // Private Methods
    
    private void UpdatePhones(IEnumerable<CompanyPhone> phones)
    {
        _phones.Clear();

        foreach (var phone in phones)
            _phones.Add(phone);
    }
}
