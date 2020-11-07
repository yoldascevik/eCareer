using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Education.ScholarshipType
{
    public class ScholarshipTypeRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}