using System;

namespace CurriculumVitae.Application.Disability.Dtos
{
    public class DisabilityInputDto
    {
        public string TypeId { get; set; }
        public float Rate { get; set; }
        public DateTime? CertificateStartDate { get; set; }
        public DateTime? CertificateExpireDate { get; set; }
        public string Notes { get; set; }
    }
}