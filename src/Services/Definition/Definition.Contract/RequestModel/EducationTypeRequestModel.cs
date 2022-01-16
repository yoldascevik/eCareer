using System.ComponentModel.DataAnnotations;

namespace Definition.Contract.RequestModel;

public class EducationTypeRequestModel
{
    [Required]
    public string Name { get; set; }
}