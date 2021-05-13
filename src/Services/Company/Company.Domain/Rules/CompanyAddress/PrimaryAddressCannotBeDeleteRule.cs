using Career.Domain.BusinessRule;
using Company.Domain.Entities;
using Company.Domain.Refs;

namespace Company.Domain.Rules.CompanyAddress
{
    public class PrimaryAddressCannotBeDeleteRule: IBusinessRule
    {
        private readonly Address _address;
        
        public PrimaryAddressCannotBeDeleteRule(Address address)
        {
            _address = address;
        }
        
        public bool IsBroken() => _address.IsPrimary;

        public string Message => "Primary address can't be deleted.";
    }
}