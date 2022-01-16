using Career.MediatR.Command;
using CurriculumVitae.Application.SocialProfile.Dtos;

namespace CurriculumVitae.Application.SocialProfile.Commands.Add;

public class AddSocialProfileCommand : ICommand<SocialProfileDto>
{
    public AddSocialProfileCommand(string cvId, SocialProfileInputDto socialProfile)
    {
        CvId = cvId;
        SocialProfile = socialProfile;
    }

    public string CvId { get; }
    public SocialProfileInputDto SocialProfile { get; }
}