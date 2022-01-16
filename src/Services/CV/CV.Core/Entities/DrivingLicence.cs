using System;
using Career.Domain;

namespace CurriculumVitae.Core.Entities;

public class DrivingLicence : EntityBase, ISoftDeletable
{
    public string Class { get; set; }
    public DateTime CertificateDate { get; set; }
    public DateTime? ExpiredDate { get; set; }
    public bool IsDeleted { get; set; }
}