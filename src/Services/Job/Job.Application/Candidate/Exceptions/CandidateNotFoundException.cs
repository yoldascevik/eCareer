using System;
using Career.Exceptions.Exceptions;

namespace Job.Application.Candidate.Exceptions
{
    public class CandidateNotFoundException: NotFoundException
    {
        public CandidateNotFoundException(Guid candidateId) : base($"Candidate is not found by id: {candidateId}")
        {
        }
    }
}