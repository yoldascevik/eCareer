using CurriculumVitae.Core.Constants;

namespace CurriculumVitae.Application.Cv.Dtos;

public class CVSummaryDto
{
    public string Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public bool DisabledPerson { get; set; }
}