using Career.Domain;
using Career.Exceptions;

namespace Job.Domain;

public abstract class IdNameRef : ValueObject
{
    public string RefId { get; protected set; }
    public string Name { get; protected set; }

    protected IdNameRef(string refId, string name)
    {
        Check.NotNullOrEmpty(refId, nameof(refId));
        Check.NotNullOrEmpty(name, nameof(name));

        RefId = refId;
        Name = name;
    }
}