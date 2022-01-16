using Career.Domain.BusinessRule;
using Career.Shared.Timing;

namespace Job.Domain.JobAggregate.Rules;

public class ValidityDateMustBeValidRule: IBusinessRule
{
    private readonly DateTime _validityDate;

    public ValidityDateMustBeValidRule(DateTime validityDate)
    {
        _validityDate = validityDate;
    }

    public bool IsBroken() => _validityDate <= Clock.Now;

    public string Message => "Job validity date is invalid!";
}