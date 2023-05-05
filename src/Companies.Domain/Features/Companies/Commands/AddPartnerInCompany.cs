using System.Text.Json.Serialization;

namespace Companies.Domain.Features.Companies.Commands;

public class AddPartnerInCompany
{
    [JsonIgnore]
    public Guid CompanyId { get; set; }
    public Guid PartnerId { get; set; }
    public int QualificationId { get; set; }
    public DateTime JoinedAt { get; set; }
}
