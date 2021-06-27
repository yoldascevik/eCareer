using System;
using System.Collections.Generic;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Application.SocialProfile;
using CurriculumVitae.Application.SocialProfile.Dtos;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Application.Cv.Dtos
{
    public class CVDto
    {
        public Guid UserId { get; set; }
        public PersonalInfoDto PersonalInfo { get; set; }
        public PersonLocationDto Location { get; set; }
        public ICollection<SocialProfileDto> SocialProfiles { get; set; }
        public ICollection<DrivingLicenceDto> DrivingLicences { get; set; }
        public ICollection<ForeignLanguage> ForeignLanguages { get; set; }
        public ICollection<EducationDto> Educations { get; set; }
        public ICollection<WorkExperienceDto> WorkExperiences { get; set; }
        public ICollection<CertificateDto> Certificates { get; set; }
        public ICollection<ReferenceDto> References { get; set; }
        public ICollection<AttachmentDto> Attachments { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}