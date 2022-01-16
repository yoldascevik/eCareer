using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.BusinessRule;
using Career.Exceptions.Exceptions;
using Job.Application.Candidate.Commands.Withdraw;
using Job.Application.Candidate.Exceptions;
using Job.Application.Job.Exceptions;
using Job.Domain.CandidateAggregate.Repositories;
using Job.Domain.CandidateAggregate.Rules;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Candidate;

public class WithdrawCandidateCommandHandlerTests
{
    private readonly IJobRepository _jobRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<WithdrawCandidateCommandHandler> _logger;

    public WithdrawCandidateCommandHandlerTests()
    {
        _jobRepository = Substitute.For<IJobRepository>();
        _candidateRepository = Substitute.For<ICandidateRepository>();
        _logger = Substitute.For<ILogger<WithdrawCandidateCommandHandler>>();
    }

    [Fact]
    public async Task Withdraw_ShouldCandidateRefIsActiveFalse_WhenSuccess()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var candidate = CandidateFaker.CreateFakeCandidate(job, true);
        var commandHandler = new WithdrawCandidateCommandHandler(_jobRepository, _candidateRepository, _logger);
        var command = new WithdrawCandidateCommand(candidate.Id);

        _candidateRepository.GetByIdAsync(candidate.Id).Returns(candidate);
        _jobRepository.GetByIdAsync(job.Id).Returns(job);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        var candidateRef = job.Candidates.FirstOrDefault(x => x.CandidateId == candidate.Id);

        Assert.NotNull(candidateRef);
        Assert.False(candidateRef.IsActive);
        await _jobRepository.Received().UpdateAsync(job.Id, job);
    }

    [Fact]
    public async Task Withdraw_ShouldBeLogInformation_WhenSuccess()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var candidate = CandidateFaker.CreateFakeCandidate(job, true);
        var commandHandler = new WithdrawCandidateCommandHandler(_jobRepository, _candidateRepository, _logger);
        var command = new WithdrawCandidateCommand(candidate.Id);

        _candidateRepository.GetByIdAsync(candidate.Id).Returns(candidate);
        _jobRepository.GetByIdAsync(job.Id).Returns(job);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        _logger.ReceivedWithAnyArgs().LogInformation("");
    }

    [Fact]
    public async Task Withdraw_ThrowNotFoundException_WhenCandidateNotExists()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var candidate = CandidateFaker.CreateFakeCandidate(job, true);
        var commandHandler = new WithdrawCandidateCommandHandler(_jobRepository, _candidateRepository, _logger);
        var command = new WithdrawCandidateCommand(candidate.Id);

        _candidateRepository.GetByIdAsync(candidate.Id).ReturnsNull();

        // Act
        var actualException = await Assert.ThrowsAsync<CandidateNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.IsType<CandidateNotFoundException>(actualException);
    }

    [Fact]
    public async Task Withdraw_ThrowNotFoundException_WhenJobNotExists()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var candidate = CandidateFaker.CreateFakeCandidate(job, true);
        var commandHandler = new WithdrawCandidateCommandHandler(_jobRepository, _candidateRepository, _logger);
        var command = new WithdrawCandidateCommand(candidate.Id);

        _jobRepository.GetByIdAsync(job.Id).ReturnsNull();
        _candidateRepository.GetByIdAsync(candidate.Id).Returns(candidate);

        // Act
        var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.IsType<JobNotFoundException>(actualException);
    }

    [Fact]
    public async Task Withdraw_ThrowNotFoundException_WhenCandidateIsNotExistInJob()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var candidate = CandidateFaker.CreateFakeCandidate(job, false);
        var commandHandler = new WithdrawCandidateCommandHandler(_jobRepository, _candidateRepository, _logger);
        var command = new WithdrawCandidateCommand(candidate.Id);

        _jobRepository.GetByIdAsync(job.Id).Returns(job);
        _candidateRepository.GetByIdAsync(candidate.Id).Returns(candidate);

        // Act
        var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.Equal($"Candidate {candidate.Id} not found for job {candidate.JobId}!", actualException.Message);
    }

    [Fact]
    public async Task Withdraw_ThrowBusinessRuleValidationException_For_CandidateMustBeActiveRule_WhenCandidateNotActive()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob(FakeJobStatus.Published);
        var candidate = CandidateFaker.CreateFakeCandidate(job, isCandidateAppliedForJob: true, isCandidateWithdrew: true);
        var commandHandler = new WithdrawCandidateCommandHandler(_jobRepository, _candidateRepository, _logger);
        var command = new WithdrawCandidateCommand(candidate.Id);

        _jobRepository.GetByIdAsync(job.Id).Returns(job);
        _candidateRepository.GetByIdAsync(candidate.Id).Returns(candidate);

        // Act
        var actualException = await Assert.ThrowsAsync<BusinessRuleValidationException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.Equal(typeof(CandidateMustBeActiveRule), actualException.BrokenRule.GetType());
    }
}