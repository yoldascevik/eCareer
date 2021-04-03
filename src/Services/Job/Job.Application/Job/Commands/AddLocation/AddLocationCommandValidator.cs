using FluentValidation;

namespace Job.Application.Job.Commands.AddLocation
{
    public class AddLocationCommandValidator: AbstractValidator<AddLocationCommand>
    {
        public AddLocationCommandValidator()
        {
            RuleFor(x => x.CountryId).NotNull().NotEmpty();
            RuleFor(x => x.CityId).NotNull().NotEmpty();
        }
    }
}