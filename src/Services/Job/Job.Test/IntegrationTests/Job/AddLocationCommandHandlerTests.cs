using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Job.Application.Job.Commands.AddLocation;
using Job.Application.Job.Dtos;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using Job.Test.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Job;

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
        var command = GetCommand(job.Id);
        var commandHandler = new AddLocationCommandHandler(_jobRepository, _mapper, _logger);

        _jobRepository.GetByIdAsync(job.Id).Returns(job);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEmpty(job.Locations);
        await _jobRepository.Received().UpdateAsync(job.Id, job);
    }

    [Fact]
    public async Task AddLocation_ShouldBeLogInformation_WhenLocationAdded()
    {
        // Arrange
        var job = JobFaker.CreateFakeJob();
        var command = GetCommand(job.Id);
        var commandHandler = new AddLocationCommandHandler(_jobRepository, _mapper, _logger);

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
        var command = GetCommand(job.Id);
        var commandHandler = new AddLocationCommandHandler(_jobRepository, _mapper, _logger);

        _jobRepository.GetByIdAsync(job.Id).ReturnsNull();

        // Act
        var actualException = await Assert.ThrowsAsync<JobNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.IsType<JobNotFoundException>(actualException);
    }

    private AddLocationCommand GetCommand(Guid jobId)
    {
        var faker = new Faker();

        return new AddLocationCommand(jobId, new JobLocationInputDto()
        {
            CountryRef = new IdNameRefDto() {RefId = faker.Random.Guid().ToString(), Name = faker.Address.Country()},
            CityRef = new IdNameRefDto() {RefId = faker.Random.Guid().ToString(), Name = faker.Address.City()}
        });
    }
}