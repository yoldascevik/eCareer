using System;
using Bogus;
using Job.Domain.JobAggregate.Refs;

namespace Job.Test.Helpers
{
    public enum FakeJobStatus
    {
        Draft,
        WaitingForApproval,
        Published
    }
    
    public class JobFaker
    {
        public static Domain.JobAggregate.Job CreateFakeJob(FakeJobStatus? status = FakeJobStatus.Draft)
        {
            var faker = new Faker();
            var job = Domain.JobAggregate.Job.Create(
                CompanyRef.Create(faker.Random.Guid(), faker.Company.CompanyName()),
                faker.Lorem.Sentence(3),
                faker.Lorem.Paragraph(),
                SectorRef.Create(faker.Random.Guid().ToString(), faker.Lorem.Word()),
                JobPositionRef.Create(faker.Random.Guid().ToString(), faker.Lorem.Word()),
                LanguageRef.Create(faker.Random.Guid().ToString(), faker.Random.RandomLocale())
            ); 

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