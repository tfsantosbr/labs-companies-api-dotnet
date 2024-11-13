using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.ValueObjects;

namespace Companies.Application.Features.Companies.Constants
{
    public static class CompanyErrors
    {
        public static Notification CompanyMustBeCreatedWithAtLeastOnePartner() =>
            new(nameof(CompanyMustBeCreatedWithAtLeastOnePartner), "The company must be created with at least one partner.");

        public static Notification CompanyCannotHaveDuplicatedPartners() =>
            new(nameof(CompanyCannotHaveDuplicatedPartners), "The company cannot have duplicate partners.");

        public static Notification CompanyCanotHaveDuplicatedPhones() =>
            new(nameof(CompanyCanotHaveDuplicatedPhones), "The company cannot have duplicate phones.");

        public static Notification CompanysPartnerNotExists() =>
            new(nameof(CompanysPartnerNotExists), "This partner not exists in this company.");

        public static Notification PartnerAlreadyLinkedWithCompany() =>
            new(nameof(PartnerAlreadyLinkedWithCompany), "This partner is already linked with this company.");
        
        public static Notification CompanyCnpjAlreadyExists(string cnpj) =>
            new(nameof(CompanyCnpjAlreadyExists), $"Company with cnpj '{cnpj}' already exists");
        
        public static Notification CompanyNameAlreadyExists(string name) =>
            new(nameof(CompanyNameAlreadyExists), $"Company with name '{name}' already exists");

        internal static Notification CompanyPhoneAlreadyExists(Phone phone)
        {
            throw new NotImplementedException();
        }

        internal static Notification CompanyNotFound(Guid companyId)
        {
            throw new NotImplementedException();
        }

        internal static Notification[] ParnterNotFound(Guid partnerId)
        {
            throw new NotImplementedException();
        }
    }
}
