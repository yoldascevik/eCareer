using Career.Domain;

namespace Company.Domain.Rules.CompanyAddress
{
    public class AddressMustHaveCityIdRule: IBusinessRule
    {
        private readonly string _cityId;
        
        public AddressMustHaveCityIdRule(string cityId)
        {
            _cityId = cityId;
        }
        
        public bool IsBroken() => string.IsNullOrEmpty(_cityId);

        public string Message => "Address must be have city id.";
    }
}