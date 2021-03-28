using System;

namespace Job.Application.Job.Dtos
{
    public class JobDto : JobInputDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Status { get; set; }
        public DateTime? RevokeDate { get; set; }
        public string RevokeReason { get; set; }
        public DateTime? ListingDate { get; set; }
        public DateTime? FirstListingDate { get; set; }
        public DateTime ValidityDate { get; set; }
    }
}