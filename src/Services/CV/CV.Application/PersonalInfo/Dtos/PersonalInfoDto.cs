using CurriculumVitae.Core.Constants;

namespace CurriculumVitae.Application.PersonalInfo.Dtos;

public class PersonalInfoDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; } = GenderType.Unspecified;
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Nationality { get; set; }
    public bool DisabledPerson { get; set; }
}