using System;
using System.Collections.Generic;
using System.Linq;
using Career.Domain;
using Career.Domain.DomainEvent;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Exceptions.Exceptions;
using Career.Shared.Timing;
using Job.Domain.Constants;
using Job.Domain.JobAggregate.DomainEvents;
using Job.Domain.JobApplicationAggregate;

namespace Job.Domain.JobAggregate
{
    public class JobAdvert : DomainEntity, IAggregateRoot
    {
        #region Ctor

        private JobAdvert(Guid companyId)
        {
            Check.NotNull(companyId, nameof(companyId));
            
            Id = Guid.NewGuid();
            CompanyId = companyId;
            Tags = new List<Tag>();
            Locations = new List<Location>();
            WorkTypes = new List<WorkType>();
            _applications = new List<JobApplication>();
            EducationLevels = new List<EducationLevel>();
            ViewingHistories = new List<ViewingHistory>();

            CreationTime = Clock.Now;
            CreatorUserId = Guid.Empty; //Todo
            
            _isCreated = true;
            AddDomainEvent(new JobAdvertCreatedEvent(this));
        }

        #endregion

        #region Private Fields

        private readonly bool _isCreated; 

        #endregion
        
        #region Properties

        public Guid Id { get; }
        public Guid CompanyId { get; private init; }
        public string LanguageId { get; private set; }
        public string SectorId { get; private set; }
        public string JobPositionId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public short? PersonCount { get; private set; }
        public bool IsCanDisabilities { get; private set; }
        public byte? MinExperienceYear { get; private set; }
        public byte? MaxExperienceYear { get; private set; }
        public GenderType Gender { get; private set; }
        public bool IsPublished { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? ListingDate { get; private set; }
        public DateTime? FirstListingDate { get; private set; }
        public DateTime ValidityDate { get; private set; }
        public DateTime CreationTime { get; private init; }
        public Guid CreatorUserId { get; private init; }
        public DateTime? LastModificationTime { get; private set; }
        public Guid? LastModifiedUserId { get; private set; }

        private List<JobApplication> _applications { get; }
        public IReadOnlyCollection<JobApplication> Applications => _applications.AsReadOnly();
        
        public ICollection<Location> Locations { get; set; }
        public ICollection<WorkType> WorkTypes { get; set; }
        public ICollection<EducationLevel> EducationLevels { get; set; }
        public ICollection<ViewingHistory> ViewingHistories { get; set; }
        public ICollection<Tag> Tags { get; set; }

        #endregion

        #region Methods

        public static JobAdvert Create(Guid companyId, string title, string description, string sectorId, string jobPositionId, string languageId)
        {
            return new JobAdvert(companyId)
                .SetTitle(title)
                .SetDescription(description)
                .SetSector(sectorId)
                .SetJobPosition(jobPositionId)
                .SetLanguage(languageId);
        }

        public JobAdvert SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new BusinessException("Title cannot be empty!");
            
            Title = title;
            SetAuditAndAddDomainEventForUpdate();
            return this;
        }

        public JobAdvert SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new BusinessException("Description cannot be empty!");

            Description = description;
            SetAuditAndAddDomainEventForUpdate();
            return this;
        }

        private void SetAuditAndAddDomainEventForUpdate()
        {
            if (!_isCreated)
            {
                LastModificationTime = Clock.Now;
                LastModifiedUserId = Guid.Empty; // Todo
                AddOrUpdateDomainEvent(new JobAdvertUpdatedEvent(this));
            }
        }

        public JobAdvert SetSector(string sectorId)
        {
            if (string.IsNullOrEmpty(sectorId))
                throw new BusinessException("Sector is required, cannot be empty!");

            SectorId = sectorId;
            SetAuditInfoForUpdate();
            return this;
        }

        public JobAdvert SetJobPosition(string jobPositionId)
        {
            if (string.IsNullOrEmpty(jobPositionId))
                throw new BusinessException("Job position is required, cannot be empty!");

            JobPositionId = jobPositionId;
            SetAuditInfoForUpdate();
            return this;
        }

        public JobAdvert SetLanguage(string languageId)
        {
            if (string.IsNullOrEmpty(languageId))
                throw new BusinessException("Language is required, cannot be empty!");

            LanguageId = languageId;
            SetAuditInfoForUpdate();
            return this;
        }

        public JobAdvert SetPersonCount(short? personCount)
        {
            if (personCount < 0)
                throw new BusinessException("The count of person is invalid");

            PersonCount = personCount;
            SetAuditInfoForUpdate();
            return this;
        }

        public JobAdvert SetCanDisabilities(bool isCanDisabilities)
        {
            IsCanDisabilities = isCanDisabilities;
            SetAuditInfoForUpdate();
            return this;
        }

        public JobAdvert SetMinExperienceYear(byte? minExperienceYear)
        {
            MinExperienceYear = minExperienceYear;
            SetAuditInfoForUpdate();
            return this;
        }
        
        public JobAdvert SetMaxExperienceYear(byte? maxExperienceYear)
        {
            MaxExperienceYear = maxExperienceYear;
            SetAuditInfoForUpdate();

            if (!_isCreated)
            {
                // MaxExperienceChangedEvent
                // JobUpdatedEvent
            }
            
            return this;
        }

        public JobAdvert SetGender(GenderType genderType)
        {
            Check.NotNull(genderType, nameof(genderType));
            Gender = genderType;
            SetAuditInfoForUpdate();
            
            // MaxExperienceChangedEvent
            // JobUpdatedEvent
            
            return this;
        }

        public void Publish(DateTime validityDate)
        {
            Check.NotNull(validityDate, nameof(validityDate));

            var normalizedValidityDate = Clock.Normalize(validityDate);
            if (normalizedValidityDate < Clock.Now)
                throw new BusinessException("Job advert validity date is invalid");
                
            IsPublished = true;
            ListingDate = Clock.Now;
            FirstListingDate ??= Clock.Now;
            ValidityDate = validityDate;
            SetAuditInfoForUpdate();
        }
        
        public void UnPublish()
        {
            IsPublished = false;
            SetAuditInfoForUpdate();
        }

        public void Delete()
        {
            IsDeleted = true;
            IsPublished = false;
            SetAuditInfoForUpdate();
        }

        public void AddApplication(JobApplication application)
        {
            Check.NotNull(application, nameof(application));

            if (_applications.Any(x => x.UserId == application.UserId))
                throw new BusinessException("You have an application for this job advert!");
            
            _applications.Add(application);
        }

        public void WithdrawApplication(JobApplication application)
        {
        }

        public void AddLocation(Location location)
        {
        }

        public void RemoveLocation(Location location)
        {
        }

        public void AddWorkType(WorkType workType)
        {
        }

        public void RemoveWorkType(WorkType workType)
        {
        }

        public void AddEducationLevel(EducationLevel educationLevel)
        {
        }

        public void RemoveEducationLevel(EducationLevel educationLevel)
        {
        }

        public void AddTag(Tag tag)
        {
        }

        public void RemoveTag(Tag tag)
        {
        }

        private void SetAuditInfoForUpdate()
        {
            LastModificationTime = Clock.Now;
            LastModifiedUserId = Guid.Empty; // Todo
        }

        #endregion

        #region DomainEntity Members

        protected override IEnumerable<object> GetIdentityMembers()
        {
            yield return new[] {Id};
        }

        #endregion
    }
}