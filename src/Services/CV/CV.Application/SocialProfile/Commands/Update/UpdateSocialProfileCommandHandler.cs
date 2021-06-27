using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.SocialProfile.Commands.Update
{
    public class UpdateSocialProfileCommandHandler : ICommandHandler<UpdateSocialProfileCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<UpdateSocialProfileCommandHandler> _logger;

        public UpdateSocialProfileCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<UpdateSocialProfileCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSocialProfileCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            if (cv.SocialProfiles.Any(x => x.Type.Id == request.SocialProfile.TypeId 
                                           && x.Id != request.SocialProfileId
                                           && x.Username.Equals(request.SocialProfile.UserName, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ItemAlreadyExistsException(request.SocialProfile.UserName);
            }
            
            var socialProfile = cv.SocialProfiles.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.SocialProfileId);
            if (socialProfile == null)
            {
                throw new SocialProfileNotFoundException(request.SocialProfileId);
            }

            _mapper.Map(request.SocialProfile, socialProfile);
            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("Social profile \"{DrivingLicenceId}\" updated", request.SocialProfileId);

            return Unit.Value;
        }
    }
}