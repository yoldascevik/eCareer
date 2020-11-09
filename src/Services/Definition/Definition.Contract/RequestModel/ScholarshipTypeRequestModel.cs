using System.ComponentModel.DataAnnotations;

namespace Definition.Contract.RequestModel
{
    public class ScholarshipTypeRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}