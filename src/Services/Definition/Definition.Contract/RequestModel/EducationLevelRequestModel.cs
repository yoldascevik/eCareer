using System.ComponentModel.DataAnnotations;

namespace Definition.Contract.RequestModel
{
    public class EducationLevelRequestModel
    {
        [Required]
        public int Level { get; set; }

        [Required]
        public string Name { get; set; }
    }
}