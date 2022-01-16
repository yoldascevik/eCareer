namespace Company.Domain.Refs;

public class CountryRef: IdNameRef
{
    private CountryRef(string refId, string name) : base(refId, name)
    {
    }

    public static CountryRef Create(string id, string name) => new CountryRef(id, name);
}