using System.Text.Json.Serialization;

using Companies.Domain.Base.Models;

using MediatR;

namespace Companies.Domain.Features.Companies.Commands;

public class AddPartner : IRequest<Response<CompanyPartner>>
{
    [JsonIgnore]
    public Guid CompanyId { get; set; }
    public Guid PartnerId { get; set; }
    public int QualificationId { get; set; }
    public DateTime JoinedAt { get; set; }
}
