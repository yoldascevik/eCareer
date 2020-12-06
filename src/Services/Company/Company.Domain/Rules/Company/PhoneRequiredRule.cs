using Career.Domain.BusinessRule;

namespace Company.Domain.Rules.Company
{
    public class PhoneRequiredRule: IBusinessRule
    {
        private readonly string _phone;

        public PhoneRequiredRule(string phone)
        {
            _phone = phone;
        }

        public bool IsBroken()
        {
            return string.IsNullOrEmpty(_phone);
        }

        public string Message => "Phone Number is required!";
    }
}