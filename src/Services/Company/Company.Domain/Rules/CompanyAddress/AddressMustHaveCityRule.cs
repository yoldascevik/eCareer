using Career.Domain.BusinessRule;
using Company.Domain.Refs;

namespace Company.Domain.Rules.CompanyAddress;

public class AddressMustHaveCityRule: IBusinessRule
{
    private readonly CityRef _cityRef;
        
    public AddressMustHaveCityRule(CityRef cityRef)
    {
        _cityRef = cityRef;
    }
        
    public bool IsBroken() => _cityRef == null;

    public string Message => "Address must be have city information.";
}