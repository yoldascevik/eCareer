using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Education.EducationLevel
{
    public class EducationLevelRequestModel
    {
        [Required]
        public int Level { get; set; }

        [Required]
        public string Name { get; set; }
    }
}