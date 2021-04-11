using System;
using System.Threading;
using System.Threading.Tasks;
using Job.Application.Tag.Commands.Delete;
using Job.Application.Tag.Exceptions;
using Job.Domain.TagAggregate.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Job.Test.IntegrationTests.Tag
{
    public class DeleteTagCommandHandlerTests
    {
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<DeleteTagCommandHandler> _logger;

        public DeleteTagCommandHandlerTests()
        {
            _tagRepository = Substitute.For<ITagRepository>();
            _logger = Substitute.For<ILogger<DeleteTagCommandHandler>>();
        }

        [Fact]
        public async Task DeleteTag_ShouldTagMarkAsDeleted_WhenTagDeleted()
        {
            // Arrange
            var tag = Domain.TagAggregate.Tag.Create("software");
            var command = new DeleteTagCommand(tag.Id);
            var commandHandler = new DeleteTagCommandHandler(_tagRepository, _logger);

            _tagRepository.GetByIdAsync(tag.Id).Returns(tag);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(tag.IsDeleted);
            await _tagRepository.Received().UpdateAsync(tag.Id, tag);
        }
        
        [Fact]
        public async Task DeleteTag_ShouldBeLogInformation_WhenTagDeleted()
        {
            // Arrange
            var tag = Domain.TagAggregate.Tag.Create("software");
            var command = new DeleteTagCommand(tag.Id);
            var commandHandler = new DeleteTagCommandHandler(_tagRepository, _logger);

            _tagRepository.GetByIdAsync(tag.Id).Returns(tag);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }
        
        [Fact]
        public async Task DeleteTag_ThrowTagNotFoundException_WhenTagNotExists()
        {
            // Arrange
            var command = new DeleteTagCommand(Guid.NewGuid());
            var commandHandler = new DeleteTagCommandHandler(_tagRepository, _logger);

            _tagRepository.GetByIdAsync(command.TagId).ReturnsNull();

            // Act
            var actualException = await Assert.ThrowsAsync<TagNotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.IsType<TagNotFoundException>(actualException);
        }
    }
}