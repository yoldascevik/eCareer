using System;
using System.Threading.Tasks;

namespace Job.Domain.JobApplicationAggregate.Repositories
{
    public interface IJobApplicationRepository
    {
        Task<JobApplication> GetByIdAsync(Guid jobApplicationId);
        Task<JobApplication> AddAsync(JobApplication jobApplication);
        Task<JobApplication> UpdateAsync(Guid jobApplicationId, JobApplication jobApplication);
        Task<bool> IsJobApplicationExists(Guid userId, Guid jobAdvertId);
    }
}