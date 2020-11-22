using System;

namespace Career.Shared.Audit
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}