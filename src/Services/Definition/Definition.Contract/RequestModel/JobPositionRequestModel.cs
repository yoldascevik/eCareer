using System.ComponentModel.DataAnnotations;

namespace Definition.Contract.RequestModel
{
    public class JobPositionRequestModel
    {
        [Required]
        public string SectorId { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}