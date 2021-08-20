using Career.MediatR.Command;

namespace CurriculumVitae.Application.SocialProfile.Commands.Delete
{
    public class DeleteSocialProfileCommand : ICommand
    {
        public DeleteSocialProfileCommand(string cvId, string socialProfileId)
        {
            CvId = cvId;
            SocialProfileId = socialProfileId;
        }

        public string CvId { get; }
        public string SocialProfileId { get; }
    }
}