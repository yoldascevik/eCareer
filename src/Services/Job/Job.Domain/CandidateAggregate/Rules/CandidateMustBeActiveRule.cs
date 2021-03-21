using Career.Domain.BusinessRule;

namespace Job.Domain.CandidateAggregate.Rules
{
    public class CandidateMustBeActiveRule: IBusinessRule
    {
        private readonly bool _isActive;

        public CandidateMustBeActiveRule(bool isActive)
        {
            _isActive = isActive;
        }

        public bool IsBroken()
        {
            return !_isActive;
        }

        public string Message => "Candidate is not active!";
    }
}