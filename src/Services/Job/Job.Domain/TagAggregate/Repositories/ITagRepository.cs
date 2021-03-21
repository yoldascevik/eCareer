using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Job.Domain.TagAggregate.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> GetByIdAsync(Guid tagId);
        Task<bool> AnyAsync(Expression<Func<Tag, bool>> condition);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag> UpdateAsync(Guid tagId, Tag tag);
    }
}