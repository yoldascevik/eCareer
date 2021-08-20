using Career.MediatR.Command;
using CurriculumVitae.Application.SocialProfile.Dtos;

namespace CurriculumVitae.Application.SocialProfile.Commands.Update
{
    public class UpdateSocialProfileCommand : ICommand
    {
        public UpdateSocialProfileCommand(string cvId, string socialProfileId, SocialProfileInputDto socialProfile)
        {
            CvId = cvId;
            SocialProfileId = socialProfileId;
            SocialProfile = socialProfile;
        }

        public string CvId { get; }
        public string SocialProfileId { get; }
        public SocialProfileInputDto SocialProfile { get; }
    }
}