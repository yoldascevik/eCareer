using CurriculumVitae.Application.DisabilityType;

namespace CurriculumVitae.Application.PersonalInfo.Dtos;

public class DisabilityDto
{
    public string Id { get; set; }
    public DisabilityTypeDto Type { get; set; } 
    public float Rate { get; set; }
    public DateTime? CertificateStartDate { get; set; }
    public DateTime? CertificateExpireDate { get; set; }
    public string Notes { get; set; }
}