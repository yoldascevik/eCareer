using Career.Domain.BusinessRule;

namespace Company.Domain.Rules.Company
{
    public class SectorIdRequiredRule: IBusinessRule
    {
        private readonly string _sectorId;

        public SectorIdRequiredRule(string sectorId)
        {
            _sectorId = sectorId;
        }

        public bool IsBroken()
        {
            return string.IsNullOrEmpty(_sectorId);
        }

        public string Message => "SectorId is required!";
    }
}