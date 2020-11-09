using System;

namespace Definition.Application.Work.JobPosition
{
    [Serializable]
    public class JobPositionDto
    {
        public string Id { get; set; }
        public string SectorId { get; set; }
        public string Name { get; set; }
    }
}