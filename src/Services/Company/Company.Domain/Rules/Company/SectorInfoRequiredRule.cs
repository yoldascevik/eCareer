using Career.Domain.BusinessRule;
using Company.Domain.ValueObjects;

namespace Company.Domain.Rules.Company
{
    public class SectorInfoRequiredRule: IBusinessRule
    {
        private readonly IdNameLookup _sectorInfo;

        public SectorInfoRequiredRule(IdNameLookup sectorInfo)
        {
            _sectorInfo = sectorInfo;
        }

        public bool IsBroken()
        {
            return _sectorInfo == null;
        }

        public string Message => "Sector info is required!";
    }
}