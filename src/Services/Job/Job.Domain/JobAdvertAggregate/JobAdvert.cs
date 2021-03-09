using System;
using System.Collections.Generic;
using System.Linq;
using Career.Domain;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Exceptions.Exceptions;
using Career.Shared.Timing;
using Job.Domain.JobAdvertAggregate.Constants;
using Job.Domain.JobAdvertAggregate.Events;
using Job.Domain.JobApplicationAggregate;

namespace Job.Domain.JobAdvertAggregate
{
    public class JobAdvert : DomainEntity, IAggregateRoot
    {
        #region Fields

        private List<Tag> _tags;
        private List<LocationRef> _locations;
        private List<WorkTypeRef> _workTypes;
        private List<ApplicationRef> _applications;
        private List<ViewingHistory> _viewingHistories;
        private List<EducationLevelRef> _educationLevels;

        private readonly bool _isCreated;

        #endregion

        #region Ctor

        private JobAdvert()
        {
            Id = Guid.NewGuid();
            _tags = new List<Tag>();
            _locations = new List<LocationRef>();
            _workTypes = new List<WorkTypeRef>();
            _applications = new List<ApplicationRef>();
            _educationLevels = new List<EducationLevelRef>();
            _viewingHistories = new List<ViewingHistory>();

            CreationTime = Clock.Now;
            CreatorUserId = Guid.Empty; //Todo
        }

        private JobAdvert(Guid companyId) : this()
        {
            Check.NotNull(companyId, nameof(companyId));

            _isCreated = true;

            CompanyId = companyId;
            AddDomainEvent(new JobAdvertCreatedEvent(this));
        }

        #endregion

        #region Properties

        public Guid Id { get; private set; }
        public Guid CompanyId { get; private set; }
        public string LanguageId { get; private set; }
        public string SectorId { get; private set; }
        public string JobPositionId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public short? PersonCount { get; private set; }
        public bool IsCanDisabilities { get; private set; }
        public byte? MinExperienceYear { get; private set; }
        public byte? MaxExperienceYear { get; private set; }
        public GenderType Gender { get; private set; } = GenderType.Unspecified;
        public bool IsPublished { get; private set; }
        public DateTime? RevokeDate { get; private set; }
        public string RevokeReason { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? ListingDate { get; private set; }
        public DateTime? FirstListingDate { get; private set; }
        public DateTime ValidityDate { get; private set; }
        public DateTime CreationTime { get; private set; }
        public Guid CreatorUserId { get; private set; }
        public DateTime? LastModificationTime { get; private set; }
        public Guid? LastModifiedUserId { get; private set; }

        public virtual IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        public virtual IReadOnlyCollection<LocationRef> Locations => _locations.AsReadOnly();
        public virtual IReadOnlyCollection<WorkTypeRef> WorkTypes => _workTypes.AsReadOnly();
        public virtual IReadOnlyCollection<ApplicationRef> Applications => _applications.AsReadOnly();
        public virtual IReadOnlyCollection<ViewingHistory> ViewingHistories => _viewingHistories.AsReadOnly();
        public virtual IReadOnlyCollection<EducationLevelRef> EducationLevels => _educationLevels.AsReadOnly();

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
            OnUpdated();

            return this;
        }

        public JobAdvert SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new BusinessException("Description cannot be empty!");

            Description = description;
            OnUpdated();

            return this;
        }

        public JobAdvert SetSector(string sectorId)
        {
            if (string.IsNullOrEmpty(sectorId))
                throw new BusinessException("Sector is required, cannot be empty!");

            SectorId = sectorId;
            OnUpdated();

            return this;
        }

        public JobAdvert SetJobPosition(string jobPositionId)
        {
            if (string.IsNullOrEmpty(jobPositionId))
                throw new BusinessException("Job position is required, cannot be empty!");

            JobPositionId = jobPositionId;
            OnUpdated();

            return this;
        }

        public JobAdvert SetLanguage(string languageId)
        {
            if (string.IsNullOrEmpty(languageId))
                throw new BusinessException("Language is required, cannot be empty!");

            LanguageId = languageId;
            OnUpdated();

            return this;
        }

        public JobAdvert SetPersonCount(short? personCount)
        {
            if (personCount < 0)
                throw new BusinessException("The count of person is invalid");

            PersonCount = personCount;
            OnUpdated();

            return this;
        }

        public JobAdvert SetCanDisabilities(bool isCanDisabilities)
        {
            IsCanDisabilities = isCanDisabilities;
            OnUpdated();

            return this;
        }

        public JobAdvert SetMinExperienceYear(byte? minExperienceYear)
        {
            if (minExperienceYear > MaxExperienceYear)
            {
                throw new BusinessException("The minimum years of experience cannot greater than the maximum years of experience.");
            }

            MinExperienceYear = minExperienceYear;
            OnUpdated();

            return this;
        }

        public JobAdvert SetMaxExperienceYear(byte? maxExperienceYear)
        {
            if (maxExperienceYear < MinExperienceYear)
            {
                throw new BusinessException("The maximum years of experience cannot less than the minimum years of experience.");
            }

            MaxExperienceYear = maxExperienceYear;
            OnUpdated();

            return this;
        }

        public JobAdvert SetGender(GenderType genderType)
        {
            Check.NotNull(genderType, nameof(genderType));
            Gender = genderType;
            OnUpdated();

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
            ValidityDate = normalizedValidityDate;

            OnUpdated();
            AddDomainEvent(new JobAdvertPublishedEvent(this));
        }

        public void Revoke(string reason)
        {
            if (string.IsNullOrEmpty(reason))
                throw new BusinessException("Revoke reason is required!");

            IsPublished = false;
            RevokeReason = reason;
            RevokeDate = Clock.Now;

            OnUpdated();
            AddDomainEvent(new JobAdvertRevokedEvent(this));
        }

        public void Delete()
        {
            IsDeleted = true;
            IsPublished = false;
            LastModificationTime = Clock.Now;
            LastModifiedUserId = Guid.Empty; // Todo
            AddDomainEvent(new JobAdvertDeletedEvent(this));
        }

        public void Apply(JobApplication jobApplication)
        {
            Check.NotNull(jobApplication, nameof(jobApplication));

            if (jobApplication.JobAdvertId != Id)
                throw new BusinessException("The application does not belong to this job advert!");

            //TODO
            // if (!IsPublished || ValidityDate <= Clock.Now)
            //     throw new BusinessException("This job advert is no longer valid.");

            if (_applications.Any(x => x.UserId == jobApplication.UserId))
                throw new BusinessException("You have an application for this job advert!");

            _applications.Add(ApplicationRef.Create(jobApplication));
            AddDomainEvent(new JobApplicationReceivedEvent(this, jobApplication));
        }

        public void WithdrawApplication(JobApplication jobApplication)
        {
            Check.NotNull(jobApplication, nameof(jobApplication));

            ApplicationRef application = _applications.FirstOrDefault(x => x.UserId == jobApplication.UserId);
            if (application == null)
                throw new NotFoundException("Job application not found!");

            application.Withdraw();
            AddDomainEvent(new JobApplicationWithdrawnEvent(this, jobApplication));
        }

        public void AddLocation(LocationRef location)
        {
            Check.NotNull(location, nameof(location));
            _locations.Add(location);
            OnUpdated();
        }

        public void RemoveLocation(LocationRef location)
        {
            Check.NotNull(location, nameof(location));

            var jobLocation = _locations.FirstOrDefault(x => x.Id == location.Id);
            if (jobLocation == null)
                throw new NotFoundException("Job location not found!");

            _locations.Remove(jobLocation);
            OnUpdated();
        }

        public void AddWorkType(WorkTypeRef workType)
        {
            Check.NotNull(workType, nameof(workType));

            if (_workTypes.Any(x => x.Id == workType.Id))
                throw new BusinessException("Work type already exist for this job advert!");

            _workTypes.Add(workType);
            OnUpdated();
        }

        public void RemoveWorkType(WorkTypeRef workType)
        {
            Check.NotNull(workType, nameof(workType));

            var jobWorkType = _workTypes.FirstOrDefault(x => x.Id == workType.Id);
            if (jobWorkType == null)
                throw new NotFoundException("Work type not found!");

            _workTypes.Remove(jobWorkType);
            OnUpdated();
        }

        public void AddEducationLevel(EducationLevelRef educationLevel)
        {
            Check.NotNull(educationLevel, nameof(educationLevel));

            if (_educationLevels.Any(x => x.Id == educationLevel.Id))
                throw new BusinessException("Education level already exist for this job advert!");

            _educationLevels.Add(educationLevel);
            OnUpdated();
        }

        public void RemoveEducationLevel(EducationLevelRef educationLevel)
        {
            Check.NotNull(educationLevel, nameof(educationLevel));

            var jobEducationLevel = _educationLevels.FirstOrDefault(x => x.Id == educationLevel.Id);
            if (jobEducationLevel == null)
                throw new NotFoundException("Education level is not found!");

            _educationLevels.Remove(jobEducationLevel);
            OnUpdated();
        }

        public void AddTag(Tag tag)
        {
            Check.NotNull(tag, nameof(tag));

            if (_tags.Any(x => x.Id == tag.Id))
                throw new BusinessException("Tag already exist for this job advert!");

            _tags.Add(tag);

            AddDomainEvent(new TagAddedToJobAdvert(this, tag));
            OnUpdated();
        }

        public void RemoveTag(Tag tag)
        {
            Check.NotNull(tag, nameof(tag));

            var jobTag = _tags.FirstOrDefault(x => x.Id == tag.Id);
            if (jobTag == null)
                throw new NotFoundException("Tag is not found!");

            _tags.Remove(jobTag);
            OnUpdated();
        }

        private void OnUpdated()
        {
            if (!_isCreated)
            {
                LastModificationTime = Clock.Now;
                LastModifiedUserId = Guid.Empty; // Todo
                AddOrUpdateDomainEvent(new JobAdvertUpdatedEvent(this));
            }
        }

        #endregion
    }
}