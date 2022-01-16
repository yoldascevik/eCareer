using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Candidate.Dtos;
using Job.Domain.CandidateAggregate.Repositories;

namespace Job.Application.Candidate.Queries.Get;

public class GetCandidatesQueryHandler : IQueryHandler<GetCandidatesQuery, PagedList<CandidateDto>>
{
    private readonly IMapper _mapper;
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidatesQueryHandler(ICandidateRepository candidateRepository, IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<CandidateDto>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        return await _candidateRepository
            .Get(request.IncludeDeactivated)
            .OrderByDescending(candidate => candidate.ApplicationDate)
            .ProjectTo<CandidateDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}