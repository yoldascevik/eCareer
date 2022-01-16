using System;
using Career.Domain;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Refs;

public class CompanyRef: ValueObject
{
    private CompanyRef(Guid refId, string name)
    {
        Check.NotNullOrEmpty(name, nameof(name));

        RefId = refId;
        Name = name;
    }

    public Guid RefId { get; private set; }
    public string Name { get; private set; }

    public static CompanyRef Create(Guid refId, string name) => new(refId, name);
}