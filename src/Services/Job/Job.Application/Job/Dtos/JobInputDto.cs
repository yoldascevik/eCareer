using Job.Domain.JobAggregate.Constants;

namespace Job.Application.Job.Dtos
{
    public class JobInputDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public short? PersonCount { get; set; }
        public bool IsCanDisabilities { get; set; }
        public byte? MinExperienceYear { get; set; }
        public byte? MaxExperienceYear { get; set; }
        public GenderType Gender { get; set; } = GenderType.Unspecified;
        public IdNameRefDto Language { get; set; }
        public IdNameRefDto Sector { get; set; }
        public IdNameRefDto JobPosition { get; set; }
    }
}