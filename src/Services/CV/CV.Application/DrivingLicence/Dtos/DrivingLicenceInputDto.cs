using System;

namespace CurriculumVitae.Application.DrivingLicence.Dtos;

public class DrivingLicenceInputDto
{
    public string Class { get; set; }
    public DateTime CertificateDate { get; set; }
    public DateTime? ExpiredDate { get; set; }
}