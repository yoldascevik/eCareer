using Career.Domain.Specifications;

namespace Company.Domain.Rules.Company;

public interface ITaxNumberUniquenessSpecification : ISpecification<ValueObjects.TaxInfo>
{
}