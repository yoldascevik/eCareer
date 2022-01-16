using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.MediatR.Query;
using CurriculumVitae.Application.CoverLetter.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.CoverLetter.Queries.GetByUserId;

public class GetCoverLettersByUserIdQueryHandler : IQueryHandler<GetCoverLettersByUserIdQuery, PagedList<CoverLetterDto>>
{
    private readonly IMapper _mapper;
    private readonly ICoverLetterRepository _coverLetterRepository;

    public GetCoverLettersByUserIdQueryHandler(ICoverLetterRepository coverLetterRepository, IMapper mapper)
    {
        _coverLetterRepository = coverLetterRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<CoverLetterDto>> Handle(GetCoverLettersByUserIdQuery request, CancellationToken cancellationToken)
    {
        PagedList<CoverLetterDto> coverLetters = await _coverLetterRepository.GetByUserId(request.UserId)
            .OrderBy(x => x.Title)
            .ProjectTo<CoverLetterDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);

        return coverLetters;
    }
}