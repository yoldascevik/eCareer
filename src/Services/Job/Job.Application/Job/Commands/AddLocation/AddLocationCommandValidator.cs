using FluentValidation;

namespace Job.Application.Job.Commands.AddLocation
{
    public class AddLocationCommandValidator: AbstractValidator<AddLocationCommand>
    {
        public AddLocationCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.LocationInputDto.CountryId).NotNull().NotEmpty();
            RuleFor(x => x.LocationInputDto.CityId).NotNull().NotEmpty();
        }
    }
}