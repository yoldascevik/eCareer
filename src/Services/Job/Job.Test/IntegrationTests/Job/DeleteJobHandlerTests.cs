using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.DeleteJob;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class DeleteJobHandlerTests
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<DeleteJobCommandHandler> _logger;

        public DeleteJobHandlerTests()
        {
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<DeleteJobCommandHandler>>();
        }
        
        [Fact]
        public async Task DeleteJobHandler_ShouldJobIsDeletedIsTrue_WhenJobDeleted()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = new DeleteJobCommand(job.Id);
            var commandHandler = new DeleteJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(job.IsDeleted);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task DeleteJobHandler_ShouldBeLogInformation_WhenJobDeleted()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = new DeleteJobCommand(job.Id);
            var commandHandler = new DeleteJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);
        
            // Act
            await commandHandler.Handle(command, CancellationToken.None);
        
            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task DeleteJobHandler_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = new DeleteJobCommand(job.Id);
            var commandHandler = new DeleteJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Job is not found by id: {job.Id}", actualException.Message);
        }
    }
}