using FluentValidation;

namespace Job.Application.Job.Commands.AddLocation
{
    public class AddLocationCommandValidator: AbstractValidator<AddLocationCommand>
    {
        public AddLocationCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.LocationInputDto)
                .NotNull()
                .ChildRules(locDto =>
                {
                    locDto.RuleFor(x => x.CountryRef)
                        .NotNull()
                        .ChildRules(countryDto =>
                        {
                            countryDto.RuleFor(x => x.RefId).NotNull().NotEmpty();
                            countryDto.RuleFor(x => x.Name).NotNull().NotEmpty();
                        });
                    
                    locDto.RuleFor(x => x.CityRef)
                        .NotNull()
                        .ChildRules(cityDto =>
                        {
                            cityDto.RuleFor(x => x.RefId).NotNull().NotEmpty();
                            cityDto.RuleFor(x => x.Name).NotNull().NotEmpty();
                        });
                });
        }
    }
}