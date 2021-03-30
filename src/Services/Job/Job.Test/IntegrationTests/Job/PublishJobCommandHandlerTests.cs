using System;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.BusinessRule;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.PublishJob;
using Job.Domain.JobAggregate.Constants;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.JobAggregate.Rules;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class PublishJobCommandHandlerTests
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<PublishJobCommandHandler> _logger;

        public PublishJobCommandHandlerTests()
        {
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<PublishJobCommandHandler>>();
        }
        
        [Fact]
        public async Task PublishJob_ShouldBeJobStatusEqualsToPublished_WhenJobPublished()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var validityDate = DateTime.Now.AddDays(10);
            var expectedJobStatus = JobStatus.Published; 
            var command = new PublishJobCommand(job.Id, validityDate);
            var commandHandler = new PublishJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedJobStatus, job.Status);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task PublishJob_ShouldBeLogInformation_WhenJobPublished()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var validityDate = DateTime.Now.AddDays(10);
            var command = new PublishJobCommand(job.Id, validityDate);
            var commandHandler = new PublishJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);
        
            // Act
            await commandHandler.Handle(command, CancellationToken.None);
        
            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task PublishJob_ThrowBusinessException_WhenJobStatusNotSuitable()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var validityDate = DateTime.Now.AddDays(10);
            var command = new PublishJobCommand(job.Id, validityDate);
            var commandHandler = new PublishJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            var actualException = await Assert.ThrowsAsync<BusinessException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal("The status of the job is not suitable.", actualException.Message);
        }
        
        [Fact]
        public async Task PublishJob_ThrowBusinessRuleValidationException_For_ValidityDateMustBeValidRule_WhenValidityDateNotValid()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var validityDate = DateTime.Now.AddDays(-1);
            var command = new PublishJobCommand(job.Id, validityDate);
            var commandHandler = new PublishJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            var actualException = await Assert.ThrowsAsync<BusinessRuleValidationException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal(typeof(ValidityDateMustBeValidRule), actualException.BrokenRule.GetType());
        }
        
        [Fact]
        public async Task PublishJob_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var validityDate = DateTime.Now.AddDays(10);
            var command = new PublishJobCommand(job.Id, validityDate);
            var commandHandler = new PublishJobCommandHandler(_jobRepository, _logger);
            
            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Job is not found by id: {job.Id}", actualException.Message);
        }
    }
}