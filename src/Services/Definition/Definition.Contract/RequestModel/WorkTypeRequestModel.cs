using System.ComponentModel.DataAnnotations;

namespace Definition.Contract.RequestModel;

public class WorkTypeRequestModel
{
    [Required]
    public string Name { get; set; }
}