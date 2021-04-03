using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.AddLocation;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class AddLocationCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<AddLocationCommandHandler> _logger;

        public AddLocationCommandHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<AddLocationCommandHandler>>();
        }

        [Fact]
        public async Task AddLocation_ShouldJobIncludeLocation_WhenLocationAdded()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddLocationCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddLocationCommand(job.Id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), null);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            var jobLocationDto = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEmpty(job.Locations);
            Assert.Equal(command.CountryId, jobLocationDto.CountryId);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task AddLocation_ShouldBeLogInformation_WhenLocationAdded()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddLocationCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddLocationCommand(job.Id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), null);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task AddLocation_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddLocationCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddLocationCommand(job.Id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), null);

            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Job is not found by id: {job.Id}", actualException.Message);
        }
    }
}