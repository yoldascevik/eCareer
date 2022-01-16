using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Tag.Dtos;
using Job.Domain.TagAggregate.Repositories;

namespace Job.Application.Tag.Queries.Get;

public class GetTagsQueryHandler : IQueryHandler<GetTagsQuery, PagedList<TagDto>>
{
    private readonly IMapper _mapper;
    private readonly ITagRepository _tagRepository;

    public GetTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _tagRepository
            .Get()
            .OrderBy(tag => tag.Name)
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}