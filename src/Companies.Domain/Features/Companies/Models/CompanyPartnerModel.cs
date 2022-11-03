namespace Companies.Domain.Features.Companies.Models;

public class CompanyPartnerModel
{
    public Guid PartnerId { get; set; }
    public int QualificationId { get; set; }
    public DateOnly JoinedAt { get; set; }
    public int QualificationCode { get; set; }
}
