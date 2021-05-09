using Career.Domain.BusinessRule;

namespace Company.Domain.Rules.CompanyAddress
{
    public class AddressTitleRequiredRule : IBusinessRule
    {
        private readonly string _title;
        
        public AddressTitleRequiredRule(string title)
        {
            _title = title;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_title);

        public string Message { get; } = "Address title is required.";
    }
}