using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Job.Domain.TagAggregate.Repositories
{
    public interface ITagRepository
    {
        IQueryable<Tag> Get();
        Task<Tag> GetByIdAsync(Guid tagId);
        Task<bool> Exists(string tagName);
        Task<bool> AnyAsync(Expression<Func<Tag, bool>> condition);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag> UpdateAsync(Guid tagId, Tag tag);
    }
}