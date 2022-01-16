using AutoMapper;
using Job.Application.Tag.Commands.Update;
using Job.Application.Tag.Exceptions;
using Job.Domain.TagAggregate.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Tag;

public class UpdateTagCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ITagRepository _tagRepository;
    private readonly ILogger<UpdateTagCommandHandler> _logger;

    public UpdateTagCommandHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _tagRepository = Substitute.For<ITagRepository>();
        _logger = Substitute.For<ILogger<UpdateTagCommandHandler>>();
    }

    [Fact]
    public async Task UpdateTag_ShouldReturnExpectedName_WhenTagUpdated()
    {
        // Arrange
        var expectedName = "changed name";
        var tag = Domain.TagAggregate.Tag.Create("software");
        var command = new UpdateTagCommand(tag.Id, expectedName);
        var commandHandler = new UpdateTagCommandHandler(_tagRepository, _mapper, _logger);

        _tagRepository.GetByIdAsync(command.TagId).Returns(tag);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedName, tag.Name);
        await _tagRepository.Received().UpdateAsync(tag.Id, tag);
    }
        
    [Fact]
    public async Task UpdateTag_ShouldBeLogInformation_WhenTagUpdated()
    {
        // Arrange
        var tag = Domain.TagAggregate.Tag.Create("software");
        var command = new UpdateTagCommand(tag.Id, "new name");
        var commandHandler = new UpdateTagCommandHandler(_tagRepository, _mapper, _logger);

        _tagRepository.GetByIdAsync(command.TagId).Returns(tag);

        // Act
        await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        _logger.ReceivedWithAnyArgs().LogInformation("");
    }
        
    [Fact]
    public async Task DeleteTag_ThrowTagNotFoundException_WhenTagNotExists()
    {
        // Arrange
        var command = new UpdateTagCommand(Guid.NewGuid(), "new name");
        var commandHandler = new UpdateTagCommandHandler(_tagRepository, _mapper, _logger);

        _tagRepository.GetByIdAsync(command.TagId).ReturnsNull();

        // Act
        var actualException = await Assert.ThrowsAsync<TagNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.IsType<TagNotFoundException>(actualException);
    }
}