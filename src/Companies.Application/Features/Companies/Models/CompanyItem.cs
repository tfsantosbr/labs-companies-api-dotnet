namespace Companies.Application.Features.Companies.Models;

public class CompanyItem
{
    public Guid Id { get; set; }
    public string Cnpj { get; set; } = default!;
    public string Name { get; set; } = default!;
}
