using System;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Career.Exceptions.Exceptions;
using Job.Application.Candidate.Dtos;
using Job.Application.Job.Commands.Apply;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job;

public class ApplyCommandHandlerTests
{
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<ApplyCommandHandler> _logger;

    public ApplyCommandHandlerTests()
    {
        _jobRepository = Substitute.For<IJobRepository>();
        _logger = Substitute.For<ILogger<ApplyCommandHandler>>();
    }

    [Fact]
    public async Task Apply_ShouldCandidateAddToJob_WhenSuccess()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var commandHandler = new ApplyCommandHandler(_jobRepository, _logger);
        var command = GetCommand(job.Id);

        _jobRepository.GetByIdAsync(job.Id).Returns(job);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Contains(job.Candidates, x => x.UserId == command.CandidateDto.UserId);
        await _jobRepository.Received().UpdateAsync(job.Id, job);
    }
        
    [Fact]
    public async Task Apply_ShouldBeLogInformation_WhenSuccess()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var command = GetCommand(job.Id);
        var commandHandler = new ApplyCommandHandler(_jobRepository, _logger);
            
        _jobRepository.GetByIdAsync(job.Id).Returns(job);
            
        // Act
        await commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        _logger.ReceivedWithAnyArgs().LogInformation("");
    }
        
    [Fact]
    public async Task Apply_ThrowNotFoundException_WhenJobNotExists()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var command = GetCommand(job.Id);
        var commandHandler = new ApplyCommandHandler(_jobRepository, _logger);
            
        _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        
        // Act
        var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.IsType<JobNotFoundException>(actualException);
    }
        
    [Fact]
    public async Task Apply_ThrowBusinessException_WhenJobStatusNotSuitable()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob();
        var command = GetCommand(job.Id);
        var commandHandler = new ApplyCommandHandler(_jobRepository, _logger);
            
        _jobRepository.GetByIdAsync(job.Id).Returns(job);

        // Act
        var actualException = await Assert.ThrowsAsync<BusinessException>(() => commandHandler.Handle(command, CancellationToken.None));
        
        // Assert
        Assert.Equal("The status of the job is not suitable.", actualException.Message);
    }

    [Fact]
    public async Task Apply_ThrowBusinessException_WhenCandidateAlreadyAppliedToJob()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var command = GetCommand(job.Id);
        var commandHandler = new ApplyCommandHandler(_jobRepository, _logger);
            
        _jobRepository.GetByIdAsync(job.Id).Returns(job);
        await commandHandler.Handle(command, CancellationToken.None);
            
        // Act
        var actualException = await Assert.ThrowsAsync<BusinessException>(() => commandHandler.Handle(command, CancellationToken.None));
        
        // Assert
        Assert.Equal("You have an application for this job!", actualException.Message);
    }

    private ApplyCommand GetCommand(Guid? jobId)
    {
        var candidateDto = new Faker<CandidateInputDto>()
            .Rules((faker, job) =>
            {
                job.UserId = faker.Random.Guid();
                job.CvId = faker.Random.Guid();
                job.CoverLetter = faker.Lorem.Paragraph(1);
            }).Generate();

        return new ApplyCommand(jobId ?? Guid.NewGuid(), candidateDto);
    }
}