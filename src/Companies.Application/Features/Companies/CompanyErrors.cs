using Companies.Application.Abstractions.Results;

namespace Companies.Application.Features.Companies
{
    public static class CompanyErrors
    {
        public static Notification CompanyMustBeCreatedWithAtLeastOnePartner() =>
            new(nameof(CompanyMustBeCreatedWithAtLeastOnePartner), "The company must be created with at least one partner");

        public static Notification CompanyCannotHaveDuplicatedPartners() =>
            new(nameof(CompanyCannotHaveDuplicatedPartners), "The company cannot have duplicate partners");

        public static Notification CompanyCanotHaveDuplicatedPhones() =>
            new(nameof(CompanyCanotHaveDuplicatedPhones), "The company cannot have duplicate phones");

        public static Notification CompanysPartnerNotExists() =>
            new(nameof(CompanysPartnerNotExists), "This partner not exists in this company");

        public static Notification PartnerAlreadyLinkedWithCompany() =>
            new(nameof(PartnerAlreadyLinkedWithCompany), "This partner is already linked with this company");
    }
}
