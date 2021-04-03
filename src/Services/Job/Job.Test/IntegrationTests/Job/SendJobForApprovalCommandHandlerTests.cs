using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.SendForApproval;
using Job.Domain.JobAggregate.Constants;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class SendJobForApprovalCommandHandlerTests
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<SendJobForApprovalCommandHandler> _logger;

        public SendJobForApprovalCommandHandlerTests()
        {
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<SendJobForApprovalCommandHandler>>();
        }
        
        [Fact]
        public async Task SendJobForApproval_ShouldBeJobStatusEqualsToWaitingForApproval_WhenJobSentForAppoval()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var expectedJobStatus = JobStatus.WaitingForApproval; 
            var command = new SendJobForApprovalCommand(job.Id);
            var commandHandler = new SendJobForApprovalCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedJobStatus, job.Status);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task SendJobForApproval_ShouldBeLogInformation_WhenJobSentForAppoval()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = new SendJobForApprovalCommand(job.Id);
            var commandHandler = new SendJobForApprovalCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            
            // Act
            await commandHandler.Handle(command, CancellationToken.None);
        
            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task SendJobForApproval_ThrowBusinessException_WhenJobStatusNotSuitable()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob(FakeJobStatus.WaitingForApproval);
            var command = new SendJobForApprovalCommand(job.Id);
            var commandHandler = new SendJobForApprovalCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            
            // Act
            var actualException = await Assert.ThrowsAsync<BusinessException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal("The status of the job is not suitable.", actualException.Message);
        }
        
        [Fact]
        public async Task SendJobForApproval_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = new SendJobForApprovalCommand(job.Id);
            var commandHandler = new SendJobForApprovalCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Job is not found by id: {job.Id}", actualException.Message);
        }
    }
}