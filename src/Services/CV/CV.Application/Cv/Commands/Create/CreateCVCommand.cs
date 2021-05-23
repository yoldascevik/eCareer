using System;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.Cv.Commands.Create
{
    public class CreateCVCommand: ICommand<CVSummaryDto>
    {
        public Guid UserId { get; set; }
        public PersonalInfoDto PersonalInfo { get; set; }
    }
}