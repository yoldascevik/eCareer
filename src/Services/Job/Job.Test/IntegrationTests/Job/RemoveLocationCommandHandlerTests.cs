using Bogus;
using Career.Exceptions.Exceptions;
using Job.Application.Job.Commands.RemoveLocation;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate;
using Job.Domain.JobAggregate.Refs;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job;

public class RemoveLocationCommandHandlerTests
{
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<RemoveLocationCommandHandler> _logger;

    public RemoveLocationCommandHandlerTests()
    {
        _jobRepository = Substitute.For<IJobRepository>();
        _logger = Substitute.For<ILogger<RemoveLocationCommandHandler>>();
    }

    [Fact]
    public async Task RemoveLocation_ShouldLocationRemovedFromJob_WhenLocationRemoved()
    {
        // Arrange
        var faker = new Faker();
        var job = JobFaker.CreateFakeJob();
        var commandHandler = new RemoveLocationCommandHandler(_jobRepository, _logger);
        var location = JobLocation.Create(
            country: CountryRef.Create(faker.Random.Guid().ToString(), faker.Address.Country()),
            city: CityRef.Create(faker.Random.Guid().ToString(), faker.Address.City())
        );
        var command = new RemoveLocationCommand(job.Id, location.Id);

        _jobRepository.GetByIdAsync(job.Id).Returns(job);
        job.AddLocation(location);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.DoesNotContain(job.Locations, x => x.Id == location.Id);
        await _jobRepository.Received().UpdateAsync(job.Id, job);
    }

    [Fact]
    public async Task RemoveLocation_ShouldBeLogInformation_WhenLocationRemoved()
    {
        // Arrange
        var faker = new Faker();
        var job = JobFaker.CreateFakeJob();
        var commandHandler = new RemoveLocationCommandHandler(_jobRepository, _logger);
        var location = JobLocation.Create(
            country: CountryRef.Create(faker.Random.Guid().ToString(), faker.Address.Country()),
            city: CityRef.Create(faker.Random.Guid().ToString(), faker.Address.City())
        );
        var command = new RemoveLocationCommand(job.Id, location.Id);

        _jobRepository.GetByIdAsync(job.Id).Returns(job);
        job.AddLocation(location);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        _logger.ReceivedWithAnyArgs().LogInformation("");
    }

    [Fact]
    public async Task RemoveLocation_ThrowNotFoundException_WhenJobNotExists()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob();
        var commandHandler = new RemoveLocationCommandHandler(_jobRepository, _logger);
        var command = new RemoveLocationCommand(job.Id, Guid.NewGuid());

        _jobRepository.GetByIdAsync(job.Id).ReturnsNull();

        // Act
        var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.IsType<JobNotFoundException>(actualException);
    }

    [Fact]
    public async Task RemoveLocation_ThrowNotFoundException_WhenLocationNotExistsInJob()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob();
        var commandHandler = new RemoveLocationCommandHandler(_jobRepository, _logger);
        var command = new RemoveLocationCommand(job.Id, Guid.NewGuid());

        _jobRepository.GetByIdAsync(job.Id).Returns(job);

        // Act
        var actualException = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.Equal($"Job location not found: {command.LocationId}", actualException.Message);
    }
}