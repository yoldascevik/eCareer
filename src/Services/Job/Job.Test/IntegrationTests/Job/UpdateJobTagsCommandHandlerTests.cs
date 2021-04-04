using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Job.Application.Job.Commands.UpdateJobTags;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.JobAggregate.Services;
using Job.Test.Helpers; 
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class UpdateJobTagsCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly IJobDomainService _jobDomainService;
        private readonly ILogger<UpdateJobTagsCommandHandler> _logger;

        public UpdateJobTagsCommandHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _jobRepository = Substitute.For<IJobRepository>();
            _jobDomainService = new JobDomainService(_jobRepository);
            _logger = Substitute.For<ILogger<UpdateJobTagsCommandHandler>>();
        }

        [Fact]
        public async Task UpdateJobTags_ShouldNewTagsAddedToJob_WhenSuccess()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var newTags = new[] {"software", "devops", "freelance"};
            var command = new UpdateJobTagsCommand(job.Id, newTags);
            var commandHandler = new UpdateJobTagsCommandHandler(_mapper, _jobRepository, _jobDomainService, _logger);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(job.Tags.Select(x=> x.Name).SequenceEqual(newTags));
            await _jobRepository.Received().UpdateAsync(job.Id, job);
        }
        
        [Fact]
        public async Task UpdateJobTags_ShouldRemoveTagsFromJob_WhenTagsDeleted()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var newTags = new[] {"software", "freelance"};
            var existsTag = new[] {"software", "devops", "freelance"};
            var command = new UpdateJobTagsCommand(job.Id, newTags);
            var commandHandler = new UpdateJobTagsCommandHandler(_mapper, _jobRepository, _jobDomainService, _logger);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            foreach (string tagName in existsTag)
            {
                job.AddTag(Domain.TagAggregate.Tag.Create(tagName));
            }

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(job.Tags.Select(x=> x.Name).SequenceEqual(newTags));
        }
        
        [Fact]
        public async Task UpdateJobTags_ShouldBeLogInformation_WhenSuccess()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var newTags = new[] {"software", "devops", "freelance"};
            var command = new UpdateJobTagsCommand(job.Id, newTags);
            var commandHandler = new UpdateJobTagsCommandHandler(_mapper, _jobRepository, _jobDomainService, _logger);

            _jobRepository.GetByIdAsync(job.Id).Returns(job);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task UpdateJobTags_ThrowNotFoundException_WhenJobNotExists()
        {
            // Arrange
            var job = JobFaker.CreateFakeJob();
            var newTags = new[] {"software", "devops", "freelance"};
            var command = new UpdateJobTagsCommand(job.Id, newTags);
            var commandHandler = new UpdateJobTagsCommandHandler(_mapper, _jobRepository, _jobDomainService, _logger);

            _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
            // Act
            var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<JobNotFoundException>(actualException);
        }
    }
}