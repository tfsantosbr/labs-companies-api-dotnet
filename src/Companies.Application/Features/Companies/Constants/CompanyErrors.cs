using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.ValueObjects;

namespace Companies.Application.Features.Companies.Constants
{
    public static class CompanyErrors
    {
        public static Error CompanyMustBeCreatedWithAtLeastOnePartner() =>
            new(nameof(CompanyMustBeCreatedWithAtLeastOnePartner), "The company must be created with at least one partner.");

        public static Error CompanyCannotHaveDuplicatedPartners() =>
            new(nameof(CompanyCannotHaveDuplicatedPartners), "The company cannot have duplicate partners.");

        public static Error CompanyCanotHaveDuplicatedPhones() =>
            new(nameof(CompanyCanotHaveDuplicatedPhones), "The company cannot have duplicate phones.");

        public static Error CompanysPartnerNotExists() =>
            new(nameof(CompanysPartnerNotExists), "This partner not exists in this company.");

        public static Error PartnerAlreadyLinkedWithCompany() =>
            new(nameof(PartnerAlreadyLinkedWithCompany), "This partner is already linked with this company.");

        public static Error CompanyCnpjAlreadyExists(string cnpj) =>
            new(nameof(CompanyCnpjAlreadyExists), $"Company with cnpj '{cnpj}' already exists");

        public static Error CompanyNameAlreadyExists(string name) =>
            new(nameof(CompanyNameAlreadyExists), $"Company with name '{name}' already exists");

        public static Error CompanyPhoneAlreadyExists(Phone phone)
            => new(nameof(CompanyPhoneAlreadyExists), $"The phone '{phone}' already exists");

        public static Error CompanyNotFound(Guid companyId)
            => new(nameof(CompanyNotFound), $"The company with id '{companyId}' was not found");

        public static Error PartnerNotFound(Guid partnerId)
            => new(nameof(PartnerNotFound), $"The partner with id '{partnerId}' was not found");
    }
}
