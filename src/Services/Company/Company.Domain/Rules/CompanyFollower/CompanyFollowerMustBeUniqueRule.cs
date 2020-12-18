using Career.Domain.BusinessRule;
using Career.Exceptions;

namespace Company.Domain.Rules.CompanyFollower
{
    public class CompanyFollowerMustBeUniqueRule: IBusinessRule
    {
        private readonly Entities.CompanyFollower _companyFollower;
        private readonly ICompanyFollowerUniquenessSpecification _specification;
        
        public CompanyFollowerMustBeUniqueRule(
            Entities.CompanyFollower companyFollower, 
            ICompanyFollowerUniquenessSpecification specification)
        {
            Check.NotNull(companyFollower, nameof(companyFollower));
            Check.NotNull(specification, nameof(specification));

            _specification = specification;
            _companyFollower = companyFollower;
        }
        
        public bool IsBroken()
        {
            return !_specification.IsSatisfiedBy(_companyFollower);
        }

        public string Message => "This user already following company!";
    }
}