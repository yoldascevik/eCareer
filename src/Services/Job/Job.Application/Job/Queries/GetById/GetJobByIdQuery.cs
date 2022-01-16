using System;
using Career.MediatR.Query;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Queries.GetById;

public class GetJobByIdQuery: IQuery<JobDetailDto>
{
    public GetJobByIdQuery(Guid jobId)
    {
        JobId = jobId;
    }

    public Guid JobId { get; }
}