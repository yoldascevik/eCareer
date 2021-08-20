using Career.MediatR.Command;

namespace CurriculumVitae.Application.SocialProfileType.Command.Delete
{
    public class DeleteSocialProfileTypeCommand : ICommand
    {
        public DeleteSocialProfileTypeCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}