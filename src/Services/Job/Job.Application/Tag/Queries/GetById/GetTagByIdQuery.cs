using Career.MediatR.Query;
using Job.Application.Tag.Dtos;

namespace Job.Application.Tag.Queries.GetById;

public class GetTagByIdQuery: IQuery<TagDto>
{
    public GetTagByIdQuery(Guid tagId)
    {
        TagId = tagId;
    }

    public Guid TagId { get; }
}