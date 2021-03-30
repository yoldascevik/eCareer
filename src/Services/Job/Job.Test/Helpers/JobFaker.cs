using System;
using Bogus;

namespace Job.Test.Helpers
{
    public enum FakeJobStatus
    {
        WaitingForApproval,
        Published
    }
    
    public class JobFaker
    {
        public static Domain.JobAggregate.Job CreateFakeJob(FakeJobStatus? status = null)
        {
            var faker = new Faker();
            var job = Domain.JobAggregate.Job.Create(
                faker.Random.Guid(),
                faker.Lorem.Sentence(3),
                faker.Lorem.Paragraph(),
                faker.Random.Guid().ToString(),
                faker.Random.Guid().ToString(),
                faker.Random.Guid().ToString());

            if (status == FakeJobStatus.WaitingForApproval)
            {
                job.SendForApproval();
            }
            else if(status == FakeJobStatus.Published)
            {
                job.SendForApproval();
                job.Publish(DateTime.Now.AddDays(5));
            }

            return job;
        }
    }
}