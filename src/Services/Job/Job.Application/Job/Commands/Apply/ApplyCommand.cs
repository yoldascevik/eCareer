using System;
using Job.Application.Candidate.Dtos;
using ICommand = Career.MediatR.Command.ICommand;

namespace Job.Application.Job.Commands.Apply;

public class ApplyCommand : ICommand
{
    public ApplyCommand(Guid jobId, CandidateInputDto candidateDto)
    {
        JobId = jobId;
        CandidateDto = candidateDto;
    }

    public Guid JobId { get; }
    public CandidateInputDto CandidateDto { get; }
}