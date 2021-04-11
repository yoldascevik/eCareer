using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Job.Application.Tag.Dtos;
using Job.Application.Tag.Exceptions;
using Job.Domain.TagAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Tag.Commands.Update
{
    public class UpdateTagCommandHandler: ICommandHandler<UpdateTagCommand, TagDto>
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<UpdateTagCommandHandler> _logger;

        public UpdateTagCommandHandler(ITagRepository tagRepository, IMapper mapper, ILogger<UpdateTagCommandHandler> logger)
        {
            _tagRepository = tagRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TagDto> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetByIdAsync(request.TagId);
            if (tag is null)
                throw new TagNotFoundException(request.TagId);

            tag.SetName(request.Name);
            await _tagRepository.UpdateAsync(tag.Id, tag);
            
            _logger.LogInformation("Tag updated : {TagId}", tag.Id);
            
            return _mapper.Map<TagDto>(tag);
        }
    }
}