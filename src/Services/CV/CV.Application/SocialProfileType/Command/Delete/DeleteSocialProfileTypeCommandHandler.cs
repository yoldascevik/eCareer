using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.SocialProfileType.Command.Delete
{
    public class DeleteSocialProfileTypeCommandHandler : ICommandHandler<DeleteSocialProfileTypeCommand>
    {
        private readonly ISocialProfileTypeRepository _socialProfileTypeRepository;
        private readonly ILogger<DeleteSocialProfileTypeCommandHandler> _logger;

        public DeleteSocialProfileTypeCommandHandler(ISocialProfileTypeRepository socialProfileTypeRepository, ILogger<DeleteSocialProfileTypeCommandHandler> logger)
        {
            _socialProfileTypeRepository = socialProfileTypeRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteSocialProfileTypeCommand request, CancellationToken cancellationToken)
        {
            var socialProfileType = await _socialProfileTypeRepository.GetByKeyAsync(request.Id);
            if (socialProfileType == null || socialProfileType.IsDeleted)
                throw new SocialProfileTypeNotFoundException(request.Id);

            socialProfileType.IsDeleted = true;
            await _socialProfileTypeRepository.UpdateAsync(socialProfileType.Id, socialProfileType);
            
            _logger.LogInformation("Social profile type \"{Name}\" is deleted", socialProfileType.Name);
            
            return Unit.Value;
        }
    }
}