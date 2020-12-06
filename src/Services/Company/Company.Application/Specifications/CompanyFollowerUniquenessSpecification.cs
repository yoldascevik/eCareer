using System;
using System.Linq.Expressions;
using Career.Domain.Specifications;
using Company.Domain.Repositories;
using Company.Domain.Rules.CompanyFollower;

namespace Company.Application.Specifications
{
    public class CompanyFollowerUniquenessSpecification: Specification<Domain.Entities.CompanyFollower>, ICompanyFollowerUniquenessSpecification
    {
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public CompanyFollowerUniquenessSpecification(ICompanyFollowerRepository companyFollowerRepository)
        {
            _companyFollowerRepository = companyFollowerRepository;
        }

        public override Expression<Func<Domain.Entities.CompanyFollower, bool>> ToExpression()
        {
            return follower => !_companyFollowerRepository.Any(x => x.UserId == follower.UserId && x.CompanyId == follower.CompanyId && !x.IsDeleted);
        }
    }
}