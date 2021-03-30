using System;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.RevokeJob;
using Job.Domain.JobAggregate.Constants;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class RevokeJobCommandHandlerTests
    {
        private readonly Faker _faker;
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<RevokeJobCommandHandler> _logger;

        public RevokeJobCommandHandlerTests()
        {
            _faker = new Faker();
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<RevokeJobCommandHandler>>();
        }
        
        [Fact]
        public async Task RevokeJob_ShouldBeJobStatusEqualsToRevoked_WhenJobRevoked()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob(FakeJobStatus.WaitingForApproval);
            var revokeReason = _faker.Lorem.Sentence(4);
            var expectedJobStatus = JobStatus.Revoked; 
            var command = new RevokeJobCommand(job.Id, revokeReason);
            var commandHandler = new RevokeJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedJobStatus, job.Status);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task RevokeJob_ShouldBeLogInformation_WhenJobRevoked()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob(FakeJobStatus.WaitingForApproval);
            var revokeReason = _faker.Lorem.Sentence(4);
            var command = new RevokeJobCommand(job.Id, revokeReason);
            var commandHandler = new RevokeJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
        
            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task RevokeJob_ShouldBeRevokeReasonExpected_WhenJobRevoked()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob(FakeJobStatus.WaitingForApproval);
            var revokeReason = _faker.Lorem.Sentence(4);
            var command = new RevokeJobCommand(job.Id, revokeReason);
            var commandHandler = new RevokeJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
        
            // Assert
            Assert.Equal(revokeReason, job.RevokeReason);
        }
        
        [Fact]
        public async Task RevokeJob_ShouldBeRevokeDateExpected_WhenJobRevoked()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob(FakeJobStatus.WaitingForApproval);
            var revokeReason = _faker.Lorem.Sentence(4);
            var command = new RevokeJobCommand(job.Id, revokeReason);
            var commandHandler = new RevokeJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);
        
            // Act
            await commandHandler.Handle(command, CancellationToken.None);
        
            // Assert
            Assert.NotNull(job.RevokeDate);
            Assert.Equal(DateTime.Now.Date, job.RevokeDate.Value.Date);
        }
        
        [Fact]
        public async Task RevokeJob_ThrowBusinessException_WhenJobStatusNotSuitable()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var revokeReason = _faker.Lorem.Sentence(4);
            var command = new RevokeJobCommand(job.Id, revokeReason);
            var commandHandler = new RevokeJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);
        
            // Act
            var actualException = await Assert.ThrowsAsync<BusinessException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal("The status of the job is not suitable.", actualException.Message);
        }
        
        [Fact]
        public async Task RevokeJob_ThrowBusinessException_WhenRevokeReasonIsEmpty()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob(FakeJobStatus.WaitingForApproval);
            var command = new RevokeJobCommand(job.Id, string.Empty);
            var commandHandler = new RevokeJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);
        
            // Act
            var actualException = await Assert.ThrowsAsync<BusinessException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal("Revoke reason is required!", actualException.Message);
        }

        [Fact]
        public async Task RevokeJob_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = new RevokeJobCommand(job.Id, string.Empty);
            var commandHandler = new RevokeJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Job is not found by id: {job.Id}", actualException.Message);
        }
    }
}