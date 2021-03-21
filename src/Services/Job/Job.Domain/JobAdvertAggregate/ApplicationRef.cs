using System;
using Career.Domain;
using Career.Exceptions;
using Job.Domain.JobApplicationAggregate;
using Job.Domain.JobApplicationAggregate.Rules;

namespace Job.Domain.JobAdvertAggregate
{
    public class ApplicationRef: ValueObject
    {
        public Guid Id { get; private init; }
        public Guid UserId { get; private init; }
        public bool IsActive { get; private set; }
        
        public static ApplicationRef Create(JobApplication jobApplication)
        {
            Check.NotNull(jobApplication, nameof(jobApplication));
            
            return new ApplicationRef()
            {
                Id = jobApplication.Id,
                UserId =  jobApplication.UserId,
                IsActive = jobApplication.IsActive
            };
        }
        
        internal void Withdraw()
        {
            CheckRule(new JobApplicationMustBeActiveRule(IsActive));
            IsActive = false;
        }
    }
}