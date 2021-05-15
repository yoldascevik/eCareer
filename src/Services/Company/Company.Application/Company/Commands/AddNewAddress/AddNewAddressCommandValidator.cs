using FluentValidation;

namespace Company.Application.Company.Commands.AddNewAddress
{
    public class AddNewAddressCommandValidator : AbstractValidator<AddNewAddressCommand>
    {
        public AddNewAddressCommandValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.AddressDto).NotNull();
            RuleFor(x => x.AddressDto.Title)
                .MaximumLength(50)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.AddressDto.Details)
                .MaximumLength(250)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.AddressDto.Country)
                .NotNull()
                .ChildRules(x =>
                {
                    x.RuleFor(c => c.RefId).MaximumLength(24).NotNull().NotEmpty();
                    x.RuleFor(c => c.Name).MaximumLength(50).NotNull().NotEmpty();
                });

            RuleFor(x => x.AddressDto.City)
                .NotNull()
                .ChildRules(x =>
                {
                    x.RuleFor(c => c.RefId).MaximumLength(24).NotNull().NotEmpty();
                    x.RuleFor(c => c.Name).MaximumLength(50).NotNull().NotEmpty();
                });
            
            RuleFor(x => x.AddressDto.District)
                .ChildRules(x =>
                {
                    x.RuleFor(c => c.RefId).MaximumLength(24).NotNull().NotEmpty();
                    x.RuleFor(c => c.Name).MaximumLength(50).NotNull().NotEmpty();
                });
        }
    }
}