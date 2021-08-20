using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.SocialProfile.Commands.Delete
{
    public class DeleteSocialProfileCommandHandler : ICommandHandler<DeleteSocialProfileCommand>
    {
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<DeleteSocialProfileCommandHandler> _logger;

        public DeleteSocialProfileCommandHandler(ICVRepository cvRepository, ILogger<DeleteSocialProfileCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteSocialProfileCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            var socialProfile = cv.SocialProfiles.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.SocialProfileId);
            if (socialProfile == null)
            {
                throw new SocialProfileNotFoundException(request.SocialProfileId);
            }

            socialProfile.IsDeleted = true;
            await _cvRepository.UpdateAsync(cv.Id, cv);
            
            _logger.LogInformation("Social profile \"{SocialProfileId}\" is deleted", socialProfile.Id);
            
            return Unit.Value;
        }
    }
}