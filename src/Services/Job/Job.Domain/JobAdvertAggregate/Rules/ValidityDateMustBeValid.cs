using System;
using Career.Domain.BusinessRule;
using Career.Shared.Timing;

namespace Job.Domain.JobAdvertAggregate.Rules
{
    public class ValidityDateMustBeValid: IBusinessRule
    {
        private readonly DateTime _validityDate;

        public ValidityDateMustBeValid(DateTime validityDate)
        {
            _validityDate = validityDate;
        }

        public bool IsBroken() => _validityDate <= Clock.Now;

        public string Message => "Job advert validity date is invalid!";
    }
}