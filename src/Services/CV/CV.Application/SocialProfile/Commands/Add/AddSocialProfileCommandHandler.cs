using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Shared.Generators;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.SocialProfile.Dtos;
using CurriculumVitae.Core.Refs;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.SocialProfile.Commands.Add
{
    public class AddSocialProfileCommandHandler : ICommandHandler<AddSocialProfileCommand, SocialProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly IStringIdGenerator _stringIdGenerator;
        private readonly ILogger<AddSocialProfileCommandHandler> _logger;
        private readonly ISocialProfileTypeRepository _socialProfileTypeRepository;

        public AddSocialProfileCommandHandler(
            ICVRepository cvRepository, 
            ISocialProfileTypeRepository socialProfileTypeRepository,
            IMapper mapper, 
            IStringIdGenerator stringIdGenerator,
            ILogger<AddSocialProfileCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _cvRepository = cvRepository;
            _stringIdGenerator = stringIdGenerator;
            _socialProfileTypeRepository = socialProfileTypeRepository;
        }

        public async Task<SocialProfileDto> Handle(AddSocialProfileCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            var socialProfileType = await _socialProfileTypeRepository.GetByKeyAsync(request.SocialProfile.TypeId);
            if (socialProfileType == null)
            {
                throw new SocialProfileNotFoundException(request.SocialProfile.TypeId);
            }

            if (cv.SocialProfiles.Any(x => x.Type.Id == request.SocialProfile.TypeId 
                                           && !x.IsDeleted
                                           && x.Username.Equals(request.SocialProfile.UserName, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ItemAlreadyExistsException(request.SocialProfile.UserName);
            }
  
            var socialProfile = _mapper.Map<Core.Entities.SocialProfile>(request.SocialProfile);
            socialProfile.Id = _stringIdGenerator.Generate();
            socialProfile.Type = _mapper.Map<SocialProfileTypeRef>(socialProfileType);

            cv.SocialProfiles.Add(socialProfile);

            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("New social profile ({DrivingLicenceId}) added to CV ({CvId})", socialProfile.Id, cv.Id);

            return _mapper.Map<SocialProfileDto>(socialProfile);
        }
    }
}