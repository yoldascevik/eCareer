using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Work.WorkType
{
    public class WorkTypeRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}