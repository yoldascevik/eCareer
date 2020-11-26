using FluentValidation;

namespace Company.Application.Commands.CompanyFollower.UnfollowCompany
{
    public class UnFollowCompanyValidator: AbstractValidator<UnfollowCompanyCommand>
    {
        public UnFollowCompanyValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}