using System;

namespace Career.Shared.Audit
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}