using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.SocialProfileType.Command.Update
{
    public class UpdateSocialProfileTypeCommandHandler : ICommandHandler<UpdateSocialProfileTypeCommand>
    {
        private readonly ISocialProfileTypeRepository _socialProfileTypeRepository;
        private readonly ILogger<UpdateSocialProfileTypeCommandHandler> _logger;

        public UpdateSocialProfileTypeCommandHandler(ISocialProfileTypeRepository socialProfileTypeRepository, ILogger<UpdateSocialProfileTypeCommandHandler> logger)
        {
            _socialProfileTypeRepository = socialProfileTypeRepository;
            _logger = logger;
        }


        public async Task<Unit> Handle(UpdateSocialProfileTypeCommand request, CancellationToken cancellationToken)
        {
            var socialProfileType = await _socialProfileTypeRepository.GetByKeyAsync(request.Id);
            if (socialProfileType == null || socialProfileType.IsDeleted)
                throw new SocialProfileTypeNotFoundException(request.Id);

            if(await _socialProfileTypeRepository.ExistsByNameAsync(request.SocialProfileType.Name, socialProfileType.Id))
                throw new ItemAlreadyExistsException(request.SocialProfileType.Name);

            socialProfileType.Name = request.SocialProfileType.Name;
            socialProfileType.ProfileUrlPrefix = request.SocialProfileType.ProfileUrlPrefix;

            await _socialProfileTypeRepository.UpdateAsync(socialProfileType.Id, socialProfileType);
            _logger.LogInformation("Social profile type {Id} is updated", socialProfileType.Id);
            
            return Unit.Value;
        }
    }
}