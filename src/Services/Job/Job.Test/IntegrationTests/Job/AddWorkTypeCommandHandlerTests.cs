using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.AddWorkType;
using Job.Application.Job.Dtos;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Refs;
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
            var command = GetCommand(job.Id);
            var expectedDto = command.WorkTypeDto;
            var commandHandler = new AddWorkTypeCommandHandler(_jobRepository, _mapper, _logger);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            _mapper.Map<IdNameRefDto>(Arg.Any<WorkTypeRef>()).Returns(expectedDto);

            // Act
            var workTypeDto = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEmpty(job.WorkTypes);
            Assert.Equal(command.WorkTypeDto.RefId, workTypeDto.RefId);
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task AddWorkType_ShouldBeLogInformation_WhenWorkTypeAdded()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = GetCommand(job.Id);
            var commandHandler = new AddWorkTypeCommandHandler(_jobRepository, _mapper, _logger);
            
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
            var command = GetCommand(job.Id);
            var commandHandler = new AddWorkTypeCommandHandler(_jobRepository, _mapper, _logger);

            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<JobNotFoundException>(actualException);
        }
        
        [Fact]
        public async Task AddWorkType_ThrowAlreadyExistsException_WhenWorkTypeAlreadyExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var command = GetCommand(job.Id);
            var commandHandler = new AddWorkTypeCommandHandler(_jobRepository, _mapper, _logger);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);
            await commandHandler.Handle(command, CancellationToken.None);
            
            // Act
            var actualException = await Assert.ThrowsAsync<AlreadyExistsException>(() => commandHandler.Handle(command, CancellationToken.None));
        
            // Assert
            Assert.IsType<AlreadyExistsException>(actualException);
        }

        private AddWorkTypeCommand GetCommand(Guid jobId)
        {
            return new AddWorkTypeCommand(jobId, new IdNameRefDto() {
                RefId = Guid.NewGuid().ToString(),
                Name = "Test work type"
            });
        }
    }
}