using Companies.Domain.Features.CompanyEmployeePositions;

namespace Companies.Domain.Features.Companies;

public class CompanyEmployee
{
    public CompanyEmployee(Guid userId, int positionId, DateOnly entryDate)
    {
        UserId = userId;
        PositionId = positionId;
        EntryDate = entryDate;
    }

    public Guid CompanyId { get; private set; }
    public Guid UserId { get; private set; }
    public int PositionId { get; private set; }
    public DateOnly EntryDate { get; private set; }
    public CompanyEmployeePosition Position { get; private set; } = default!;

    public void Update(int positionId, DateOnly entryDate)
    {
        PositionId = positionId;
        EntryDate = entryDate;
    }
}
