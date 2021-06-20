using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Application.Cv.Commands.UpdateLocation
{
    public class UpdateLocationCommand : ICommand
    {
        public UpdateLocationCommand(string id, PersonLocationDto location)
        {
            CvId = id;
            Location = location;
        }

        public string CvId { get; }
        public PersonLocationDto Location { get; }
    }
}