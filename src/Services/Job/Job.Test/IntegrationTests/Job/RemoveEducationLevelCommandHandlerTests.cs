using System;
using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.RemoveEducationLevel;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class RemoveEducationLevelCommandHandlerTests
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<RemoveEducationLevelCommandHandler> _logger;

        public RemoveEducationLevelCommandHandlerTests()
        {
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<RemoveEducationLevelCommandHandler>>();
        }

        [Fact]
        public async Task RemoveEducationLevel_ShouldEducationLevelRemovedFromJob_WhenEducationLevelRemoved()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new RemoveEducationLevelCommandHandler(_jobRepository, _logger);
            var educationLevel = EducationLevelRef.Create(Guid.NewGuid().ToString(), "Test education level");
            var command = new RemoveEducationLevelCommand(job.Id, educationLevel.RefId);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            job.AddEducationLevel(educationLevel);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.DoesNotContain(job.EducationLevels, x => x.RefId == educationLevel.RefId);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }

        [Fact]
        public async Task RemoveEducationLevel_ShouldBeLogInformation_WhenEducationLevelRemoved()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new RemoveEducationLevelCommandHandler(_jobRepository, _logger);
            var educationLevel = EducationLevelRef.Create(Guid.NewGuid().ToString(), "Test education level");
            var command = new RemoveEducationLevelCommand(job.Id, educationLevel.RefId);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            job.AddEducationLevel(educationLevel);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }

        [Fact]
        public async Task RemoveEducationLevel_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new RemoveEducationLevelCommandHandler(_jobRepository, _logger);
            var command = new RemoveEducationLevelCommand(job.Id, Guid.NewGuid().ToString());

            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();

            // Act
            var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<JobNotFoundException>(actualException);
        }

        [Fact]
        public async Task RemoveEducationLevel_ThrowNotFoundException_WhenEducationLevelNotExistsInJob()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new RemoveEducationLevelCommandHandler(_jobRepository, _logger);
            var command = new RemoveEducationLevelCommand(job.Id, Guid.NewGuid().ToString());

            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal($"Education level {command.EducationLevelId} is not found!", actualException.Message);
        }
    }
}