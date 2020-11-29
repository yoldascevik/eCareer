using Career.Domain;
using Career.Exceptions;

namespace Company.Domain.Rules.CompanyAddress
{
    public class AddressMustBeValidRule: IBusinessRule
    {
        private readonly Values.CompanyAddress _companyAddress;
        private readonly IValidAddressSpecification _validAddressSpecification;

        public AddressMustBeValidRule(
            Values.CompanyAddress companyAddress, 
            IValidAddressSpecification validAddressSpecification)
        {
            Check.NotNull(companyAddress, nameof(companyAddress));
            Check.NotNull(validAddressSpecification, nameof(validAddressSpecification));
            
            _companyAddress = companyAddress;
            _validAddressSpecification = validAddressSpecification;
        }

        public bool IsBroken() => !_validAddressSpecification.IsSatisfiedBy(_companyAddress);

        public string Message => "Address info is not valid!";
    }
}