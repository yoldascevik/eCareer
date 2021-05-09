using Career.Domain.BusinessRule;

namespace Company.Domain.Rules.CompanyAddress
{
    public class AddressDetailsRequiredRule : IBusinessRule
    {
        private readonly string _details;
        
        public AddressDetailsRequiredRule(string details)
        {
            _details = details;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_details);

        public string Message { get; } = "Address details is required.";
    }
}