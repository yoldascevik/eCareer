using FluentValidation;
using Job.Application.Job.Validators;

namespace Job.Application.Job.Commands.AddLocation
{
    public class AddLocationCommandValidator: AbstractValidator<AddLocationCommand>
    {
        public AddLocationCommandValidator()
        {
            var idNameRefDtoValidator = new IdNameRefDtoValidator();
            
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.LocationInputDto)
                .NotNull()
                .ChildRules(locDto =>
                {
                    locDto.RuleFor(x => x.CountryRef).SetValidator(idNameRefDtoValidator);
                    locDto.RuleFor(x => x.CityRef).SetValidator(idNameRefDtoValidator);
                });
        }
    }
}