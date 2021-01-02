using System;
using System.Collections.Generic;
using Career.Domain;
using Career.Domain.Entities;
using Career.Shared.Timing;
using Job.Domain.Constants;

namespace Job.Domain.Entities
{
    public class JobAdvert : DomainEntity<Guid>, IAggregateRoot
    {
        #region Ctor

        private JobAdvert()
        {
            Id = Guid.NewGuid();
            Tags = new List<JobTag>();
            Locations = new List<JobLocation>();
            WorkTypes = new List<JobWorkType>();
            Applications = new List<JobApplication>();
            EducationLevels = new List<JobEducationLevel>();
            ViewingHistories = new List<JobViewingHistory>();
        }

        #endregion

        #region Properties

        public Guid CompanyId { get; private set; }
        public string LanguageId { get; private set; }
        public string SectorId { get; private set; }
        public string JobPositionId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public short? PersonCount { get; private set; }
        public bool? IsCanDisabilities { get; private set; }
        public byte MinExperienceYear { get; private set; }
        public byte? MaxExperienceYear { get; private set; }
        public GenderType Gender { get; private set; }
        public bool IsPublished { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? ListingDate { get; private set; }
        public DateTime? FirstListingDate { get; private set; }
        public DateTime ValidityDate { get; private set; }
        public DateTime CreationTime { get; private set; }
        public Guid CreatorUserId { get; private set; }
        public DateTime? LastModificationTime { get; private set; }
        public Guid? LastModifiedUserId { get; private set; }
        public ICollection<JobApplication> Applications { get; set; }
        public ICollection<JobLocation> Locations { get; set; }
        public ICollection<JobWorkType> WorkTypes { get; set; }
        public ICollection<JobEducationLevel> EducationLevels { get; set; }
        public ICollection<JobTag> Tags { get; set; }
        public ICollection<JobViewingHistory> ViewingHistories { get; set; }

        #endregion

        #region Methods

        public JobAdvert Create(Guid companyId, string languageId, string sectorId, string jobPositionId,
            string title, string description, short? personCount, bool? isCanDisabilities, GenderType genderType,
            DateTime validityDate, byte minExperienceYear, byte? maxExperienceYear)
        {
            return new JobAdvert()
            {
                Title = title,
                Description = description,
                CompanyId = companyId,
                LanguageId = languageId,
                SectorId = sectorId,
                JobPositionId = jobPositionId,
                Gender = genderType,
                PersonCount = personCount,
                ValidityDate = validityDate,
                IsCanDisabilities = isCanDisabilities,
                MinExperienceYear = minExperienceYear,
                MaxExperienceYear = maxExperienceYear,
                CreatorUserId = Guid.Empty, // TODO
                CreationTime = Clock.Now
            };
        }

        #endregion
    }
}