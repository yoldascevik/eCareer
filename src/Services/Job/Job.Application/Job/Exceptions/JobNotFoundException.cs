using System;
using Career.Exceptions.Exceptions;

namespace Job.Application.Job.Exceptions;

public class JobNotFoundException: NotFoundException
{
    public JobNotFoundException(Guid jobid) : base($"Job is not found by id: {jobid}")
    {
    }
}