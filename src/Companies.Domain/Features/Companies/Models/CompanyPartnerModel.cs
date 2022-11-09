namespace Companies.Domain.Features.Companies.Models;

public class CompanyPartnerModel
{
    public Guid PartnerId { get; set; }
    public int QualificationId { get; set; }
    public DateTime JoinedAt { get; set; }
}
