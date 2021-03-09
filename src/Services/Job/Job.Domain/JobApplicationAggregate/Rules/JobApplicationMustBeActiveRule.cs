using Career.Domain.BusinessRule;

namespace Job.Domain.JobApplicationAggregate.Rules
{
    public class JobApplicationMustBeActiveRule: IBusinessRule
    {
        private readonly bool _isActive;

        public JobApplicationMustBeActiveRule(bool isActive)
        {
            _isActive = isActive;
        }

        public bool IsBroken()
        {
            return !_isActive;
        }

        public string Message => "Job application is not active!";
    }
}