using System;

namespace CurriculumVitae.Application.Certificate.Dtos;

public class CertificateDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Institution { get; set; }
    public DateTime Date { get; set; }
}