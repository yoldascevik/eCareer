using Career.MediatR.Command;
using CurriculumVitae.Application.SocialProfileType.Dtos;

namespace CurriculumVitae.Application.SocialProfileType.Command.Update;

public class UpdateSocialProfileTypeCommand : ICommand
{
    public UpdateSocialProfileTypeCommand(string id, SocialProfileTypeInputDto socialProfileType)
    {
        Id = id;
        SocialProfileType = socialProfileType;
    }

    public string Id { get; }
    public SocialProfileTypeInputDto SocialProfileType { get; }
}