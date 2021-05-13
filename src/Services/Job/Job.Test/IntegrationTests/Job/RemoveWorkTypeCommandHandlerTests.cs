using System;
using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.RemoveWorkType;
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
    public class RemoveWorkTypeCommandHandlerTests
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<RemoveWorkTypeCommandHandler> _logger;

        public RemoveWorkTypeCommandHandlerTests()
        {
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<RemoveWorkTypeCommandHandler>>();
        }

        [Fact]
        public async Task RemoveWorkType_ShouldWorkTypeRemovedFromJob_WhenWorkTypeRemoved()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new RemoveWorkTypeCommandHandler(_jobRepository, _logger);
            var workType = WorkTypeRef.Create(Guid.NewGuid().ToString(), "Test work type");
            var command = new RemoveWorkTypeCommand(job.Id, workType.RefId);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            job.AddWorkType(workType);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.DoesNotContain(job.WorkTypes, x => x.RefId == workType.RefId);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }

        [Fact]
        public async Task RemoveWorkType_ShouldBeLogInformation_WhenWorkTypeRemoved()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new RemoveWorkTypeCommandHandler(_jobRepository, _logger);
            var workType = WorkTypeRef.Create(Guid.NewGuid().ToString(), "Test work type");
            var command = new RemoveWorkTypeCommand(job.Id, workType.RefId);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            job.AddWorkType(workType);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }

        [Fact]
        public async Task RemoveWorkType_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new RemoveWorkTypeCommandHandler(_jobRepository, _logger);
            var command = new RemoveWorkTypeCommand(job.Id, Guid.NewGuid().ToString());

            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();

            // Act
            var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<JobNotFoundException>(actualException);
        }

        [Fact]
        public async Task RemoveWorkType_ThrowNotFoundException_WhenWorkTypeNotExistsInJob()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new RemoveWorkTypeCommandHandler(_jobRepository, _logger);
            var command = new RemoveWorkTypeCommand(job.Id, Guid.NewGuid().ToString());

            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal($"Work type not found: {command.WorkTypeId}", actualException.Message);
        }
    }
}