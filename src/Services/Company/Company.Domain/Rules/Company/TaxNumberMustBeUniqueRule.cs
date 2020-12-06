using Career.Domain.BusinessRule;
using Career.Exceptions;

namespace Company.Domain.Rules.Company
{
    internal class TaxNumberMustBeUniqueRule : IBusinessRule
    {
        private readonly Values.TaxInfo _taxInfo;
        private readonly ITaxNumberUniquenessSpecification _taxNumberUniquenessSpecification;

        public TaxNumberMustBeUniqueRule(
            Values.TaxInfo taxInfo,
            ITaxNumberUniquenessSpecification taxNumberUniquenessSpecification)
        {
            Check.NotNull(taxInfo, nameof(taxInfo));
            Check.NotNull(taxNumberUniquenessSpecification, nameof(taxNumberUniquenessSpecification));
            
            _taxInfo = taxInfo;
            _taxNumberUniquenessSpecification = taxNumberUniquenessSpecification;
        }

        public bool IsBroken() => !_taxNumberUniquenessSpecification.IsSatisfiedBy(_taxInfo);

        public string Message => "Tax number already exists.";
    }
}