using System;

namespace Career.Domain.Audit
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}