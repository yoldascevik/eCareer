using Career.MediatR.Command;
using CurriculumVitae.Application.Reference.Dtos;

namespace CurriculumVitae.Application.Reference.Commands.Add
{
    public class AddReferenceCommand : ICommand<ReferenceDto>
    {
        public AddReferenceCommand(string cvId, ReferenceInputDto reference)
        {
            CvId = cvId;
            Reference = reference;
        }

        public string CvId { get; }
        public ReferenceInputDto Reference { get; }
    }
}