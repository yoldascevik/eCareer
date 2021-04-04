using System.Collections.Generic;

namespace Job.Application.Job.Dtos
{
    public class JobDetailDto: JobDto
    {
        public List<TagDto> Tags { get; set; }
        public List<WorkTypeDto> WorkTypes { get; set; }
        public List<CandidateSummaryDto> Candidates { get; set; }
        public List<JobLocationDto> Locations { get; set; }
        public List<EducationLevelDto> EducationLevels { get; set; }
    }
}