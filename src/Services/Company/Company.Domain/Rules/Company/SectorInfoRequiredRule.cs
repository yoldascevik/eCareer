using Career.Domain.BusinessRule;
using Company.Domain.Refs;

namespace Company.Domain.Rules.Company;

public class SectorInfoRequiredRule: IBusinessRule
{
    private readonly IdNameRef _sectorInfo;

    public SectorInfoRequiredRule(IdNameRef sectorInfo)
    {
        _sectorInfo = sectorInfo;
    }

    public bool IsBroken()
    {
        return _sectorInfo == null;
    }

    public string Message => "Sector info is required!";
}