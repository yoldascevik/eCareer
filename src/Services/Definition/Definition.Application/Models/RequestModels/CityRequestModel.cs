using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Models.RequestModels
{
    public class CityRequestModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string CountryId { get; set; }
        
        [Required]
        public string CityCode { get; set; }
    }
}