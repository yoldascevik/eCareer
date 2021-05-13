using System;
using System.Collections.Generic;
using System.Linq;
using Career.Domain;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Exceptions.Exceptions;
using Career.Shared.Timing;
using Job.Domain.CandidateAggregate;
using Job.Domain.JobAggregate.Constants;
using Job.Domain.JobAggregate.Events;
using Job.Domain.JobAggregate.Rules;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate
{
    public class Job : DomainEntity, IAggregateRoot
    {
        #region Fields

        private List<TagRef> _tags;
        private List<JobLocation> _locations;
        private List<WorkTypeRef> _workTypes;
        private List<CandidateRef> _candidates;
        private List<ViewingHistory> _viewingHistories;
        private List<EducationLevelRef> _educationLevels;

        private readonly bool _isCreated;

        #endregion

        #region Ctor

        private Job()
        {
            Id = Guid.NewGuid();
            _tags = new List<TagRef>();
            _locations = new List<JobLocation>();
            _workTypes = new List<WorkTypeRef>();
            _candidates = new List<CandidateRef>();
            _educationLevels = new List<EducationLevelRef>();
            _viewingHistories = new List<ViewingHistory>();

            CreationTime = Clock.Now;
            CreatorUserId = Guid.Empty; //Todo
        }

        private Job(Guid companyId) : this()
        {
            Check.NotNull(companyId, nameof(companyId));

            _isCreated = true;

            CompanyId = companyId;
            AddDomainEvent(new JobCreatedEvent(this));
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
        public JobStatus Status { get; private set; } = JobStatus.Draft;
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

        public virtual IReadOnlyCollection<TagRef> Tags => _tags.AsReadOnly();
        public virtual IReadOnlyCollection<JobLocation> Locations => _locations.AsReadOnly();
        public virtual IReadOnlyCollection<WorkTypeRef> WorkTypes => _workTypes.AsReadOnly();
        public virtual IReadOnlyCollection<CandidateRef> Candidates => _candidates.AsReadOnly();
        public virtual IReadOnlyCollection<ViewingHistory> ViewingHistories => _viewingHistories.AsReadOnly();
        public virtual IReadOnlyCollection<EducationLevelRef> EducationLevels => _educationLevels.AsReadOnly();

        #endregion

        #region Methods

        public static Job Create(Guid companyId, string title, string description, string sectorId, string jobPositionId, string languageId)
        {
            return new Job(companyId)
                .SetTitle(title)
                .SetDescription(description)
                .SetSector(sectorId)
                .SetJobPosition(jobPositionId)
                .SetLanguage(languageId);
        }

        public Job SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new BusinessException("Title cannot be empty!");

            Title = title;
            OnUpdated();

            return this;
        }

        public Job SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new BusinessException("Description cannot be empty!");

            Description = description;
            OnUpdated();

            return this;
        }

        public Job SetSector(string sectorId)
        {
            if (string.IsNullOrEmpty(sectorId))
                throw new BusinessException("Sector is required, cannot be empty!");

            SectorId = sectorId;
            OnUpdated();

            return this;
        }

        public Job SetJobPosition(string jobPositionId)
        {
            if (string.IsNullOrEmpty(jobPositionId))
                throw new BusinessException("Job position is required, cannot be empty!");

            JobPositionId = jobPositionId;
            OnUpdated();

            return this;
        }

        public Job SetLanguage(string languageId)
        {
            if (string.IsNullOrEmpty(languageId))
                throw new BusinessException("Language is required, cannot be empty!");

            LanguageId = languageId;
            OnUpdated();

            return this;
        }

        public Job SetPersonCount(short? personCount)
        {
            if (personCount < 0)
                throw new BusinessException("The count of person is invalid");

            PersonCount = personCount;
            OnUpdated();

            return this;
        }

        public Job SetCanDisabilities(bool isCanDisabilities)
        {
            IsCanDisabilities = isCanDisabilities;
            OnUpdated();

            return this;
        }

        public Job SetMinExperienceYear(byte? minExperienceYear)
        {
            if (minExperienceYear > MaxExperienceYear)
            {
                throw new BusinessException("The minimum years of experience cannot greater than the maximum years of experience.");
            }

            MinExperienceYear = minExperienceYear;
            OnUpdated();

            return this;
        }

        public Job SetMaxExperienceYear(byte? maxExperienceYear)
        {
            if (maxExperienceYear < MinExperienceYear)
            {
                throw new BusinessException("The maximum years of experience cannot less than the minimum years of experience.");
            }

            MaxExperienceYear = maxExperienceYear;
            OnUpdated();

            return this;
        }

        public Job SetGender(GenderType genderType)
        {
            Check.NotNull(genderType, nameof(genderType));
            Gender = genderType;
            OnUpdated();

            return this;
        }
        
        public Job SetValidityDate(DateTime validityDate)
        {
            var normalizedValidityDate = Clock.Normalize(validityDate);
            CheckRule(new ValidityDateMustBeValidRule(normalizedValidityDate));
            
            ValidityDate = normalizedValidityDate;
            
            OnUpdated();
            AddDomainEvent(new JobValidityDateChangedEvent(this));

            return this;
        }

        public void SendForApproval()
        {
            if (Status == JobStatus.Published || Status == JobStatus.WaitingForApproval)
                throw new BusinessException("The status of the job is not suitable.");
            
            Status = JobStatus.WaitingForApproval;
            
            OnUpdated();
            AddDomainEvent(new JobSentForApprovalEvent(this));
        }
        
        public void Publish(DateTime validityDate)
        {
            if (Status == JobStatus.Draft || Status == JobStatus.Published)
                throw new BusinessException("The status of the job is not suitable.");
            
            SetValidityDate(validityDate);
            
            Status = JobStatus.Published;
            ListingDate = Clock.Now;
            FirstListingDate ??= Clock.Now;
            
            OnUpdated();
            AddDomainEvent(new JobPublishedEvent(this));
        }

        public void Revoke(string reason)
        {
            if (Status != JobStatus.Published && Status != JobStatus.WaitingForApproval)
                throw new BusinessException("The status of the job is not suitable.");
            
            if (string.IsNullOrEmpty(reason))
                throw new BusinessException("Revoke reason is required!");

            Status = JobStatus.Revoked;
            RevokeReason = reason;
            RevokeDate = Clock.Now;

            OnUpdated();
            AddDomainEvent(new JobRevokedEvent(this));
        }

        public void MarkAsDelete()
        {
            IsDeleted = true;
            LastModificationTime = Clock.Now;
            LastModifiedUserId = Guid.Empty; // Todo
            AddDomainEvent(new JobDeletedEvent(this));
        }

        public void Apply(Candidate candidate)
        {
            Check.NotNull(candidate, nameof(candidate));

            if (candidate.JobId != Id)
                throw new BusinessException("Candidate does not belong to this job!");

            if (Status != JobStatus.Published)
                 throw new BusinessException("The status of the job is not suitable.");

            if (_candidates.Any(x => x.UserId == candidate.UserId))
                throw new BusinessException("You have an application for this job!");

            _candidates.Add(CandidateRef.Create(candidate));
            AddDomainEvent(new CandidateAddedEvent(this, candidate));
        }

        public void WithdrawCandidate(Candidate candidate)
        {
            Check.NotNull(candidate, nameof(candidate));

            CandidateRef candidateRef = _candidates.FirstOrDefault(x => x.CandidateId == candidate.Id);
            if (candidateRef == null)
                throw new NotFoundException($"Candidate {candidate.Id} not found for job {candidate.JobId}!");

            candidateRef.Withdraw();
            AddDomainEvent(new CandidateWithdrewEvent(this, candidate));
        }

        public void AddLocation(JobLocation jobLocation)
        {
            Check.NotNull(jobLocation, nameof(jobLocation));

            if (_locations.Any(x=> x.CountryRef.RefId == jobLocation.CountryRef.RefId && x.CityRef.RefId == jobLocation.CityRef.RefId))
                throw new AlreadyExistsException("Location already exists in job.");
            
            _locations.Add(jobLocation);
            OnUpdated();
        }

        public void RemoveLocation(Guid locationId)
        {
            var jobLocation = _locations.FirstOrDefault(x => x.Id == locationId);
            if (jobLocation == null)
                throw new NotFoundException($"Job location not found: {locationId}");

            _locations.Remove(jobLocation);
            OnUpdated();
        }

        public void AddWorkType(WorkTypeRef workType)
        {
            Check.NotNull(workType, nameof(workType));

            if (_workTypes.Any(x => x.RefId == workType.RefId))
                throw new AlreadyExistsException($"Work type {workType.RefId} already exist for this job!");

            _workTypes.Add(workType);
            OnUpdated();
        }

        public void RemoveWorkType(string workTypeId)
        {
            var jobWorkType = _workTypes.FirstOrDefault(x => x.RefId == workTypeId);
            if (jobWorkType == null)
                throw new NotFoundException($"Work type not found: {workTypeId}");

            _workTypes.Remove(jobWorkType);
            OnUpdated();
        }

        public void AddEducationLevel(EducationLevelRef educationLevel)
        {
            Check.NotNull(educationLevel, nameof(educationLevel));

            if (_educationLevels.Any(x => x.RefId == educationLevel.RefId))
                throw new AlreadyExistsException($"Education level {educationLevel.RefId} already exist for this job!");

            _educationLevels.Add(educationLevel);
            OnUpdated();
        }

        public void RemoveEducationLevel(string educationLevelId)
        {
            var jobEducationLevel = _educationLevels.FirstOrDefault(x => x.RefId == educationLevelId);
            if (jobEducationLevel == null)
                throw new NotFoundException($"Education level {educationLevelId} is not found!");

            _educationLevels.Remove(jobEducationLevel);
            OnUpdated();
        }

        public void AddTag(Tag tag)
        {
            Check.NotNull(tag, nameof(tag));

            if (_tags.Any(t=> t.TagId == tag.Id))
                throw new AlreadyExistsException("Tag already exist for this job!");

            _tags.Add(TagRef.CreateFromTag(tag));

            AddDomainEvent(new TagAddedToJobEvent(this, tag));
            OnUpdated();
        }

        public void RemoveTag(TagRef tag)
        {
            Check.NotNull(tag, nameof(tag));

            var tagRef = _tags.FirstOrDefault(t => t.TagId == tag.TagId);
            if (tagRef is null)        
                throw new NotFoundException("Tag is not found!");

            _tags.Remove(tagRef);
            OnUpdated();
        }
        
        public void RemoveTag(Tag tag)
        {
            Check.NotNull(tag, nameof(tag));
            RemoveTag(TagRef.CreateFromTag(tag));
        }

        private void OnUpdated()
        {
            if (!_isCreated)
            {
                LastModificationTime = Clock.Now;
                LastModifiedUserId = Guid.Empty; // Todo
                AddOrUpdateDomainEvent(new JobUpdatedEvent(this));
            }
        }

        #endregion
    }
}