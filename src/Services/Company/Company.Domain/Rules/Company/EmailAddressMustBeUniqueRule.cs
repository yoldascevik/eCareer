
using Career.Domain.BusinessRule;
using Career.Exceptions;

namespace Company.Domain.Rules.Company
{
    internal class EmailAddressMustBeUniqueRule : IBusinessRule
    {
        private readonly string _emailAddress;
        private readonly IEmailAddressUniquenessSpecification _emailAddressUniquenessSpecification;

        public EmailAddressMustBeUniqueRule(string emailAddress, IEmailAddressUniquenessSpecification emailAddressUniquenessSpecification)
        {
            Check.NotNullOrEmpty(emailAddress, nameof(emailAddress));
            Check.NotNull(emailAddressUniquenessSpecification, nameof(emailAddressUniquenessSpecification));
            
            _emailAddress = emailAddress;
            _emailAddressUniquenessSpecification = emailAddressUniquenessSpecification;
        }

        public bool IsBroken() => !_emailAddressUniquenessSpecification.IsSatisfiedBy(_emailAddress);

        public string Message => $"Email address already registered: {_emailAddress}";
    }
}