using System.ComponentModel.DataAnnotations;

namespace Definition.Contract.RequestModel
{
    public class SectorRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}