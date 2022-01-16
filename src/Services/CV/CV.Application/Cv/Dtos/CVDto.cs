using System;
using System.Collections.Generic;
using CurriculumVitae.Application.Attachment.Dtos;
using CurriculumVitae.Application.Certificate.Dtos;
using CurriculumVitae.Application.DrivingLicence.Dtos;
using CurriculumVitae.Application.Education.Dtos;
using CurriculumVitae.Application.LanguageSkill.Dtos;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Application.Reference.Dtos;
using CurriculumVitae.Application.SocialProfile.Dtos;
using CurriculumVitae.Application.WorkExperience.Dtos;

namespace CurriculumVitae.Application.Cv.Dtos;

public class CVDto
{
    public Guid UserId { get; set; }
    public PersonalInfoDto PersonalInfo { get; set; }
    public PersonLocationDto Location { get; set; }
    public ICollection<SocialProfileDto> SocialProfiles { get; set; }
    public ICollection<DrivingLicenceDto> DrivingLicences { get; set; }
    public ICollection<LanguageSkillDto> LanguageSkills { get; set; }
    public ICollection<EducationDto> Educations { get; set; }
    public ICollection<WorkExperienceDto> WorkExperiences { get; set; }
    public ICollection<CertificateDto> Certificates { get; set; }
    public ICollection<ReferenceDto> References { get; set; }
    public ICollection<AttachmentDto> Attachments { get; set; }
    public DateTime LastModificationTime { get; set; }
}