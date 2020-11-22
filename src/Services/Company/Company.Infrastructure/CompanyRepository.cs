using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Company.Domain.Repository;

namespace Company.Infrastructure
{
    public class CompanyRepository: ICompanyRepository
    {
        public IQueryable<Domain.Company> Get()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Domain.Company> Get(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Domain.Company>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Domain.Company>> GetAsync(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Domain.Company GetByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> GetByKeyAsync(object key)
        {
            throw new NotImplementedException();
        }

        public bool Any()
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<long> CountAsync(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Domain.Company First()
        {
            throw new NotImplementedException();
        }

        public Domain.Company First(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> FirstAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> FirstAsync(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Domain.Company FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public Domain.Company FirstOrDefault(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> FirstOrDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> FirstOrDefaultAsync(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Domain.Company Single()
        {
            throw new NotImplementedException();
        }

        public Domain.Company Single(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> SingleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> SingleAsync(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Domain.Company SingleOrDefault()
        {
            throw new NotImplementedException();
        }

        public Domain.Company SingleOrDefault(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> SingleOrDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> SingleOrDefaultAsync(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Domain.Company Add(Domain.Company item)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> AddAsync(Domain.Company item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Domain.Company> items)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(IEnumerable<Domain.Company> items)
        {
            throw new NotImplementedException();
        }

        public Domain.Company Update(object key, Domain.Company item)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Company> UpdateAsync(object key, Domain.Company item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object key)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(object key)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Expression<Func<Domain.Company, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsTaxNumberExistsAsync(string taxNumber, string countryId, Guid companyId = default)
        {
            throw new NotImplementedException();
        }
    }
}