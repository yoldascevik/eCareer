using System;

namespace Career.Domain.Audit
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}