using System;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.Cv.Commands.Create
{
    public class CreateCVCommand: ICommand<CVSummaryDto>
    {
        public Guid UserId { get; set; }
        public PersonalInfoInputDto PersonalInfo { get; set; }
        public PersonLocationDto Location { get; set; }
    }
}