using FluentValidation;

namespace Company.Application.CompanyFollower.Commands.UnfollowCompany;

public class UnFollowCompanyValidator: AbstractValidator<UnfollowCompanyCommand>
{
    public UnFollowCompanyValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.CompanyId).NotEmpty();
    }
}