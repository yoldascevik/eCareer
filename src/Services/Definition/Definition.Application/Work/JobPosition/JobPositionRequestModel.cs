using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Work.JobPosition
{
    public class JobPositionRequestModel
    {
        [Required]
        public string SectorId { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}