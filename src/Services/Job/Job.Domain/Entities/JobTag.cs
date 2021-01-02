using System;

namespace Job.Domain.Entities
{
    public class JobTag
    {
        public Guid JobAdvertId { get; set; }
        public JobAdvert JobAdvert { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}