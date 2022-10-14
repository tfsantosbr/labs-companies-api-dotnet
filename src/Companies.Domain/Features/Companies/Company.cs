using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.Companies.Enums;
using Companies.Domain.Features.CompanyMainActivities;

namespace Companies.Domain.Features.Companies;

public class Company
{
    private readonly List<CompanyPartner> _partners = new List<CompanyPartner>();
    private readonly List<CompanyEmployee> _employees = new List<CompanyEmployee>();

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
    public IReadOnlyCollection<CompanyEmployee> Employees => _employees.AsReadOnly();

    public void Update(
        string name, CompanyLegalNatureType legalNature, int mainActivityId,
        Address address, DateTime createdAt, DateTime updatedAt)
    {
        Name = name;
        LegalNature = legalNature;
        MainActivityId = mainActivityId;
        Address = address;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public void AddPartner(CompanyPartner partner) => _partners.Add(partner);
    public CompanyPartner? GetPartner(Guid userId) => _partners.FirstOrDefault(p => p.UserId == userId);
    public void RemovePartner(CompanyPartner partner) => _partners.Remove(partner);

    public void AddEmployee(CompanyEmployee employee) => _employees.Add(employee);
    public CompanyEmployee? GetEmployee(Guid userId) => _employees.FirstOrDefault(p => p.UserId == userId);
    public void RemoveEmployee(CompanyEmployee employee) => _employees.Remove(employee);
}
