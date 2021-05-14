using System.Collections.Generic;
using Job.Application.Tag.Dtos;

namespace Job.Application.Job.Dtos
{
    public class JobDetailDto: JobDto
    {
        public List<TagDto> Tags { get; set; }
        public List<IdNameRefDto> WorkTypes { get; set; }
        public List<CandidateSummaryDto> Candidates { get; set; }
        public List<JobLocationDto> Locations { get; set; }
        public List<IdNameRefDto> EducationLevels { get; set; }
    }
}