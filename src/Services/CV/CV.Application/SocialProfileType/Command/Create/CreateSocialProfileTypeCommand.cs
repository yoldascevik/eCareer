using Career.MediatR.Command;
using CurriculumVitae.Application.SocialProfileType.Dtos;

namespace CurriculumVitae.Application.SocialProfileType.Command.Create
{
    public class CreateSocialProfileTypeCommand: SocialProfileTypeInputDto, ICommand<SocialProfileTypeDto>
    {
        
    }
}