using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.AddWorkType;
using Job.Application.Job.Dtos;
using Job.Domain.JobAggregate;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class AddWorkTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<AddWorkTypeCommandHandler> _logger;

        public AddWorkTypeCommandHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<AddWorkTypeCommandHandler>>();
        }

        [Fact]
        public async Task AddWorkType_ShouldJobIncludeWorkType_WhenWorkTypeAdded()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddWorkTypeCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddWorkTypeCommand(job.Id, Guid.NewGuid().ToString(), "Test work type");
            var expectedDto = new WorkTypeDto()
            {
                Id = command.WorkTypeId,
                Name = command.Name
            };

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            _mapper.Map<WorkTypeDto>(Arg.Any<WorkTypeRef>()).Returns(expectedDto);

            // Act
            var workTypeDto = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEmpty(job.WorkTypes);
            Assert.Equal(command.WorkTypeId, workTypeDto.Id);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task AddWorkType_ShouldBeLogInformation_WhenWorkTypeAdded()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddWorkTypeCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddWorkTypeCommand(job.Id, Guid.NewGuid().ToString(), "Test work type");
            
            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task AddWorkType_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddWorkTypeCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddWorkTypeCommand(job.Id, Guid.NewGuid().ToString(), "Test work type");

            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.Equal($"Job is not found by id: {job.Id}", actualException.Message);
        }
        
        [Fact]
        public async Task AddWorkType_ThrowAlreadyExistsException_WhenWorkTypeAlreadyExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var commandHandler = new AddWorkTypeCommandHandler(_jobRepository, _mapper, _logger);
            var command = new AddWorkTypeCommand(job.Id, Guid.NewGuid().ToString(), "Test work type");

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            await commandHandler.Handle(command, CancellationToken.None);
            
            // Act
            var actualException = await Assert.ThrowsAsync<AlreadyExistsException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.IsType<AlreadyExistsException>(actualException);
        }
    }
}