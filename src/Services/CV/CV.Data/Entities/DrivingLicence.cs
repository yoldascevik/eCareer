using System;
using Career.Domain;
using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class DrivingLicence: Document, ISoftDeletable
    {
        public string Class { get; set; }
        public DateTime CertificateDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}