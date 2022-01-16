using System;

namespace CurriculumVitae.Application.Certificate.Dtos;

public class CertificateInputDto
{
    public string Name { get; set; }
    public string Institution { get; set; }
    public DateTime Date { get; set; }
}