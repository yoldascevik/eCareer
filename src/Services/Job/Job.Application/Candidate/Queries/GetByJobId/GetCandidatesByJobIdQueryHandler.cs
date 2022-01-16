using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Candidate.Dtos;
using Job.Domain.CandidateAggregate.Repositories;

namespace Job.Application.Candidate.Queries.GetByJobId;

public class GetCandidatesByJobIdQueryHandler : IQueryHandler<GetCandidatesByJobIdQuery, PagedList<CandidateDto>>
{
    private readonly IMapper _mapper;
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidatesByJobIdQueryHandler(ICandidateRepository candidateRepository, IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<CandidateDto>> Handle(GetCandidatesByJobIdQuery request, CancellationToken cancellationToken)
    {
        return await _candidateRepository.GetByJobId(request.JobId, request.IncludeDeactivated)
            .OrderByDescending(x => x.ApplicationDate)
            .ProjectTo<CandidateDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request.PaginationFilter);
    }
}