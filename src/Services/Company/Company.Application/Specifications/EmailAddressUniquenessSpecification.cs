using System.Linq.Expressions;
using Career.Domain.Specifications;
using Career.Exceptions;
using Company.Domain.Repositories;
using Company.Domain.Rules.Company;

namespace Company.Application.Specifications;

public class EmailAddressUniquenessSpecification : Specification<string>, IEmailAddressUniquenessSpecification
{
    private readonly Guid _companyId;
    private readonly ICompanyRepository _companyRepository;

    public EmailAddressUniquenessSpecification(ICompanyRepository companyRepository, Guid companyId = default)
    {
        Check.NotNull(companyRepository, nameof(companyRepository));

        _companyId = companyId;
        _companyRepository = companyRepository;
    }

    public override Expression<Func<string, bool>> ToExpression()
    {
        return email => !_companyRepository.IsCompanyEmailExists(email, _companyId);
    }
}