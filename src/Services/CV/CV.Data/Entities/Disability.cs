using System;
using Career.Domain;
using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class Disability: Document, ISoftDeletable
    {
        public string TypeId { get; set; }
        public float Rate { get; set; }
        public DateTime? CertificateStartDate { get; set; }
        public DateTime? CertificateExpiretDate { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
    }
}