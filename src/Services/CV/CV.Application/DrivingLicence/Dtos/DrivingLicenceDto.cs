namespace CurriculumVitae.Application.DrivingLicence.Dtos;

public class DrivingLicenceDto
{
    public string Id { get; set; }
    public string Class { get; set; }
    public DateTime CertificateDate { get; set; }
    public DateTime? ExpiredDate { get; set; }
}