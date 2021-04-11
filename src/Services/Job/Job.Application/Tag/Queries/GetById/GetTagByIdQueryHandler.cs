using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using Job.Application.Tag.Dtos;
using Job.Application.Tag.Exceptions;
using Job.Domain.TagAggregate.Repositories;

namespace Job.Application.Tag.Queries.GetById
{
    public class GetTagByIdQueryHandler: IQueryHandler<GetTagByIdQuery, TagDto>
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;

        public GetTagByIdQueryHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<TagDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetByIdAsync(request.TagId);
            if (tag == null)
                throw new TagNotFoundException(request.TagId);
            
            return _mapper.Map<TagDto>(tag);
        }
    }
}