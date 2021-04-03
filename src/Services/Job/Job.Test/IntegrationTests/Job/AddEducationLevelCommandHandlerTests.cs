using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.AddEducationLevel;
using Job.Application.Job.Dtos;
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
    public class AddEducationLevelCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<AddEducationLevelCommandHandler> _logger;

        public AddEducationLevelCommandHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<AddEducationLevelCommandHandler>>();
        }

        [Fact]
        public async Task AddEducationLevel_ShouldJobIncludeEducationLevel_WhenEducationLevelAdded()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddEducationLevelCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddEducationLevelCommand(job.Id, Guid.NewGuid().ToString(), "Test education level");
            var expectedDto = new EducationLevelDto() { Id = command.EducationLevelId, Name = command.Name };

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            _mapper.Map<EducationLevelDto>(Arg.Any<EducationLevelRef>()).Returns(expectedDto);

            // Act
            var workTypeDto = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEmpty(job.EducationLevels);
            Assert.Equal(command.EducationLevelId, workTypeDto.Id);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task AddEducationLevel_ShouldBeLogInformation_WhenEducationLevelAdded()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddEducationLevelCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddEducationLevelCommand(job.Id, Guid.NewGuid().ToString(), "Test education level");
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task AddEducationLevel_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddEducationLevelCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddEducationLevelCommand(job.Id, Guid.NewGuid().ToString(), "Test education level");

            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<JobNotFoundException>(actualException);
        }
        
        [Fact]
        public async Task AddEducationLevel_ThrowAlreadyExistsException_WhenEducationLevelAlreadyExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddEducationLevelCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddEducationLevelCommand(job.Id, Guid.NewGuid().ToString(), "Test education level");

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            await commandHandler.Handle(command, CancellationToken.None);
            
            // Act
            var actualException = await Assert.ThrowsAsync<AlreadyExistsException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.IsType<AlreadyExistsException>(actualException);
        }
    }
}