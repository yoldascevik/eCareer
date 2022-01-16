using System.Diagnostics.CodeAnalysis;
using Job.Application.Tag.Dtos;
using Job.Domain.JobAggregate.Constants;

namespace Job.Application.Job.Dtos;

[SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
public class JobDetailDto
{
    public Guid Id { get; set; }
    public CompanyRefDto Company { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public short? PersonCount { get; set; }
    public bool IsCanDisabilities { get; set; }
    public byte? MinExperienceYear { get; set; }
    public byte? MaxExperienceYear { get; set; }
    public GenderType Gender { get; set; } = GenderType.Unspecified;
    public DateTime? ListingDate { get; set; }
    public DateTime? FirstListingDate { get; set; }
    public DateTime ValidityDate { get; set; }
    public DateTime? RevokeDate { get; set; }
    public string RevokeReason { get; set; }
    public IdNameRefDto Language { get; set; }
    public IdNameRefDto Sector { get; set; }
    public IdNameRefDto JobPosition { get; set; }
    public List<TagDto> Tags { get; set; } 
    public List<IdNameRefDto> WorkTypes { get; set; } 
    public List<CandidateSummaryDto> Candidates { get; set; }
    public List<JobLocationDto> Locations { get; set; }
    public List<IdNameRefDto> EducationLevels { get; set; }
}