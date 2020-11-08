using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Work.Sector
{
    public class SectorRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}