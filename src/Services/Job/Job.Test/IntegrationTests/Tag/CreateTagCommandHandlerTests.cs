using System;
using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Job.Application.Tag.Commands.Create;
using Job.Domain.TagAggregate.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Job.Test.IntegrationTests.Tag;

public class CreateTagCommandHandlerTests
{
    private readonly ITagRepository _tagRepository;
    private readonly ILogger<CreateTagCommandHandler> _logger;

    public CreateTagCommandHandlerTests()
    {
        _tagRepository = Substitute.For<ITagRepository>();
        _logger = Substitute.For<ILogger<CreateTagCommandHandler>>();
    }

    [Fact]
    public async Task CreateTag_ShouldReturnTagId_WhenTagCreated()
    {
        // Arrange
        var command = new CreateTagCommand() {Name = "software"};
        var commandHandler = new CreateTagCommandHandler(_tagRepository, _logger);

        _tagRepository.Exists(command.Name).Returns(false);

        // Act
        var tagId = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, tagId);
        await _tagRepository.Received().AddAsync(Arg.Any<Domain.TagAggregate.Tag>());
    }
        
    [Fact]
    public async Task CreateTag_ShouldBeLogInformation_WhenTagCreated()
    {
        // Arrange
        var command = new CreateTagCommand() {Name = "software"};
        var commandHandler = new CreateTagCommandHandler(_tagRepository, _logger);

        _tagRepository.Exists(command.Name).Returns(false);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        _logger.ReceivedWithAnyArgs().LogInformation("");
    }
        
    [Fact]
    public async Task CreateTag_ThrowAlreadyExistsException_WhenTagAlreadyExists()
    {
        // Arrange
        var command = new CreateTagCommand() {Name = "software"};
        var commandHandler = new CreateTagCommandHandler(_tagRepository, _logger);

        _tagRepository.Exists(command.Name).Returns(true);

        // Act
        var actualException = await Assert.ThrowsAsync<AlreadyExistsException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.IsType<AlreadyExistsException>(actualException);
    }
}