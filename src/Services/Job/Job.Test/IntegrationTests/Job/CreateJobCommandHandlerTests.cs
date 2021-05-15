using System;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Job.Application.Job.Commands.Create;
using Job.Application.Job.Dtos;
using Job.Domain.JobAggregate.Constants;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class CreateJobCommandHandlerTests
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<CreateJobCommandHandler> _logger;

        public CreateJobCommandHandlerTests()
        {
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<CreateJobCommandHandler>>();
        }

        [Fact]
        public async Task CreateJob_ShouldReturnCreatedJobId_WhenJobCreated()
        {
            // Arrange
            var command = GetCommand();
            var commandHandler = new CreateJobCommandHandler(_jobRepository, _logger);

            // Act
            Guid jobId = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, jobId);
            await _jobRepository.Received().AddAsync(Arg.Any<Domain.JobAggregate.Job>());
        }

        [Fact]
        public async Task CreateJob_ShouldBeLogInformation_WhenJobCreated()
        {
            // Arrange
            var command = GetCommand();
            var commandHandler = new CreateJobCommandHandler(_jobRepository, _logger);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }

        private CreateJobCommand GetCommand()
        {
            var jobDto = new Faker<JobInputDto>()
                .Rules((faker, job) =>
                {
                    job.Title = faker.Lorem.Sentence(3);
                    job.Description = faker.Lorem.Paragraph();
                    job.Gender = faker.PickRandom<GenderType>();
                    job.PersonCount = faker.Random.Short(0, 50);
                    job.IsCanDisabilities = faker.Random.Bool();
                    job.MinExperienceYear = faker.Random.Byte(0, 20);
                    job.MaxExperienceYear = faker.Random.Byte(job.MinExperienceYear.Value, 20);
                    job.Sector = new IdNameRefDto {RefId = faker.Random.Guid().ToString(), Name = faker.Lorem.Word()};
                    job.JobPosition = new IdNameRefDto {RefId = faker.Random.Guid().ToString(), Name = faker.Lorem.Word()};
                    job.Language = new IdNameRefDto {RefId = faker.Random.Guid().ToString(), Name = faker.Random.RandomLocale()};
                }).Generate();

            return new CreateJobCommand()
            {
                Company = new CompanyRefDto()
                {
                    RefId = Guid.NewGuid(), 
                    Name = new Faker().Company.CompanyName()
                },
                Job = jobDto
            };
        }
    }
}