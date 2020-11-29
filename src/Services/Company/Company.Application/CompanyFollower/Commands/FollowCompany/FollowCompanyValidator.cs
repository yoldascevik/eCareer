using FluentValidation;

namespace Company.Application.CompanyFollower.Commands.FollowCompany
{
    public class FollowCompanyValidator : AbstractValidator<FollowCompanyCommand>
    {
        public FollowCompanyValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}