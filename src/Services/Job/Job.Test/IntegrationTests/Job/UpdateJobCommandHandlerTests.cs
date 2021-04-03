using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.Update;
using Job.Application.Job.Dtos;
using Job.Domain.JobAggregate.Constants;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class UpdateJobCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<UpdateJobCommandHandler> _logger;

        public UpdateJobCommandHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<UpdateJobCommandHandler>>();
        }
                
        [Fact]
        public async Task UpdateJob_ShouldBePropertiesEqualExpected_WhenJobUpdated()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = GetCommand(job.Id);
            var commandHandler = new UpdateJobCommandHandler(_jobRepository, _mapper, _logger);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            
            // Act
            await commandHandler.Handle(command, CancellationToken.None); 

            // Assert
            PropertyInfo[] inputDtoProperties = command.Job.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var inputDtoProperty in inputDtoProperties)
            {
                var responseProperty = job.GetType().GetProperty(inputDtoProperty.Name);
                if (responseProperty != null)
                {
                    object expectedValue = inputDtoProperty.GetValue(command.Job);
                    object actualValue = responseProperty.GetValue(job);
                    
                    Assert.Equal(expectedValue, actualValue);
                }
            }
        }

        [Fact]
        public async Task UpdateJob_ShouldBeLogInformation_WhenJobUpdated()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = GetCommand(job.Id);
            var commandHandler = new UpdateJobCommandHandler(_jobRepository, _mapper, _logger);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            
            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task UpdateJob_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = GetCommand(job.Id);
            var commandHandler = new UpdateJobCommandHandler(_jobRepository, _mapper, _logger);

            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Job is not found by id: {job.Id}", actualException.Message);
        }

        private UpdateJobCommand GetCommand(Guid jobId)
        {
            var jobDto = new Faker<JobInputDto>()
                .Rules((faker, job) =>
                {
                    job.Title = faker.Lorem.Sentence(3);
                    job.Description = faker.Lorem.Paragraph();
                    job.Gender = faker.PickRandom<GenderType>();
                    job.LanguageId = faker.Random.Guid().ToString();
                    job.PersonCount = faker.Random.Short(0, 50);
                    job.SectorId = faker.Random.Guid().ToString();
                    job.IsCanDisabilities = faker.Random.Bool();
                    job.JobPositionId = faker.Random.Guid().ToString();
                    job.MinExperienceYear = faker.Random.Byte(0, 20);
                    job.MaxExperienceYear = faker.Random.Byte(job.MinExperienceYear.Value, 20);
                }).Generate();

            return new UpdateJobCommand(jobId, jobDto);
        }
    }
}