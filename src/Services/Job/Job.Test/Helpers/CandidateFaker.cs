using Bogus;
using Job.Domain.CandidateAggregate;

namespace Job.Test.Helpers;

public class CandidateFaker
{
    /// <summary>
    /// Create new fake candidate for a job.
    /// </summary>
    /// <param name="job">Applied Job. Can be null.</param>
    /// <param name="isCandidateAppliedForJob">Candidate already applied to job.</param>
    /// <param name="isCandidateWithdrew">Has the candidate withdrawn before?</param>
    /// <returns>Fake candidate</returns>
    public static Candidate CreateFakeCandidate(Domain.JobAggregate.Job job, bool isCandidateAppliedForJob, bool isCandidateWithdrew = false)
    {
        var faker = new Faker();
        var candidate = Candidate.Create(job, faker.Random.Guid(), faker.Random.Guid(), faker.Lorem.Paragraph(1));

        if (isCandidateAppliedForJob)
        {
            job.Apply(candidate);
                
            if (isCandidateWithdrew)
                job.WithdrawCandidate(candidate);
        }
        else if (isCandidateWithdrew)
        {
            throw new ArgumentException($"Argument must be true for {nameof(isCandidateWithdrew)}", nameof(isCandidateAppliedForJob));
        }
            
        return candidate;
    }
}