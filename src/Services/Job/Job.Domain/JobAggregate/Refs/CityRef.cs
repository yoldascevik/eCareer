namespace Job.Domain.JobAggregate.Refs;

public class CityRef: IdNameRef
{
    private CityRef(string refId, string name) : base(refId, name)
    {
    }
        
    public static CityRef Create(string refId, string name) => new (refId, name);
}