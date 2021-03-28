using Bogus;

namespace Job.Test.Helpers
{
    public class JobFaker
    {
        public static Domain.JobAggregate.Job CreateFakeJob()
        {
            var faker = new Faker();
            var job = Domain.JobAggregate.Job.Create(
                faker.Random.Guid(),
                faker.Lorem.Sentence(3),
                faker.Lorem.Paragraph(),
                faker.Random.Guid().ToString(),
                faker.Random.Guid().ToString(),
                faker.Random.Guid().ToString());

            return job;
        }
    }
}