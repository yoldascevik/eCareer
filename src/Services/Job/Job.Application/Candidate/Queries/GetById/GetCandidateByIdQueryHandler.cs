using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using Job.Application.Candidate.Dtos;
using Job.Application.Candidate.Exceptions;
using Job.Domain.CandidateAggregate.Repositories;

namespace Job.Application.Candidate.Queries.GetById;

public class GetCandidateByIdQueryHandler: IQueryHandler<GetCandidateByIdQuery, CandidateDto>
{
    private readonly IMapper _mapper;
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidateByIdQueryHandler(ICandidateRepository candidateRepository, IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
    }

    public async Task<CandidateDto> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);
        if (candidate == null)
            throw new CandidateNotFoundException(request.CandidateId);
            
        return _mapper.Map<CandidateDto>(candidate);
    }
}