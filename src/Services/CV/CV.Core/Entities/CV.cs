using System;
using System.Collections.Generic;
using Career.Domain;
using CurriculumVitae.Data.Entities;

namespace CurriculumVitae.Core.Entities
{
    public class CV : EntityBase, ISoftDeletable
    {
        public CV()
        {
            SocialProfiles = new List<SocialProfile>();
            DrivingLicences = new List<DrivingLicence>();
            LanguageSkills = new List<LanguageSkill>();
            Educations = new List<Education>();
            WorkExperiences = new List<WorkExperience>();
            Certificates = new List<Certificate>();
            References = new List<Reference>();
            Attachments = new List<Attachment>();
        }

        public Guid UserId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public PersonLocation Location { get; set; }
        public ICollection<SocialProfile> SocialProfiles { get; set; }
        public ICollection<DrivingLicence> DrivingLicences { get; set; }
        public ICollection<LanguageSkill> LanguageSkills { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
        public ICollection<Certificate> Certificates { get; set; }
        public ICollection<Reference> References { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public DateTime LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}