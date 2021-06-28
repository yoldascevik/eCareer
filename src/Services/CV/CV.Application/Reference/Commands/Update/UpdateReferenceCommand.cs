using Career.MediatR.Command;
using CurriculumVitae.Application.Reference.Dtos;

namespace CurriculumVitae.Application.Reference.Commands.Update
{
    public class UpdateReferenceCommand : ICommand
    {
        public UpdateReferenceCommand(string cvId, string referenceId, ReferenceInputDto reference)
        {
            CvId = cvId;
            ReferenceId = referenceId;
            Reference = reference;
        }

        public string CvId { get; }
        public string ReferenceId { get; }
        public ReferenceInputDto Reference { get; }
    }
}