namespace Companies.Domain.Features.CompanyEmployeePositions;

public class CompanyEmployeePosition
{
    public CompanyEmployeePosition(int id, string description)
    {
        Id = id;
        Description = description;
    }

    private CompanyEmployeePosition()
    {
    }

    public int Id { get; private set; }
    public string Description { get; private set; } = default!;
}
