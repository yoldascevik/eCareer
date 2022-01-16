namespace Company.Domain.Refs;

public class CityRef: IdNameRef
{
    private CityRef(string refId, string name) : base(refId, name)
    {
    }

    public static CityRef Create(string id, string name) => new CityRef(id, name);
}