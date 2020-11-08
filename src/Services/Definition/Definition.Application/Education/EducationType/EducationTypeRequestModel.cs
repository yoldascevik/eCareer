using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Education.EducationType
{
    public class EducationTypeRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}