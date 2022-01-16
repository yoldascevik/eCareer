using AutoMapper;
using Job.Application.Candidate.Dtos;

namespace Job.Application.Candidate;

public class CandidateMappingProfile : Profile
{
    public CandidateMappingProfile()
    {
        CreateMap<Domain.CandidateAggregate.Candidate, CandidateDto>();
    }
}