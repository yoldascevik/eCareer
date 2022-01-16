using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using CurriculumVitae.Application.SocialProfileType.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.SocialProfileType.Command.Create;

public class CreateSocialProfileTypeCommandHandler : ICommandHandler<CreateSocialProfileTypeCommand, SocialProfileTypeDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSocialProfileTypeCommandHandler> _logger;
    private readonly ISocialProfileTypeRepository _socialProfileTypeRepository;

    public CreateSocialProfileTypeCommandHandler(
        ISocialProfileTypeRepository socialProfileTypeRepository, 
        IMapper mapper, 
        ILogger<CreateSocialProfileTypeCommandHandler> logger)
    {
        _socialProfileTypeRepository = socialProfileTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SocialProfileTypeDto> Handle(CreateSocialProfileTypeCommand request, CancellationToken cancellationToken)
    {
        if (await _socialProfileTypeRepository.ExistsByNameAsync(request.Name))
            throw new ItemAlreadyExistsException(request.Name);

        var socialProfileType = new Core.Entities.SocialProfileType()
        {
            Name = request.Name,
            ProfileUrlPrefix = request.ProfileUrlPrefix
        };
            
        await _socialProfileTypeRepository.AddAsync(socialProfileType);
        _logger.LogInformation("Social profile type \"{Name}\" created", request.Name);

        return _mapper.Map<SocialProfileTypeDto>(socialProfileType);
    }
}