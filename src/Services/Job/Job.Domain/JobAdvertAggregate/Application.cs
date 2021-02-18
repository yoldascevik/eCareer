using System;
using Career.Domain;
using Career.Exceptions;
using Job.Domain.JobApplicationAggregate;

namespace Job.Domain.JobAdvertAggregate
{
    public class Application: ValueObject
    {
        public Guid Id { get; private init; }
        public Guid UserId { get; private init; }
        public bool IsActive { get; private set; }
        
        public static Application Create(JobApplication jobApplication)
        {
            Check.NotNull(jobApplication, nameof(jobApplication));
            
            return new Application()
            {
                Id = jobApplication.Id,
                UserId = jobApplication.UserId,
                IsActive = jobApplication.IsActive
            };
        }
        
        public void Withdraw()
        {
            IsActive = false;
        }
    }
}