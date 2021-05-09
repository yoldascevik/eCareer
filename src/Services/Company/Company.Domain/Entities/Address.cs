using System;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Shared.Timing;
using Company.Domain.Refs;
using Company.Domain.Rules.CompanyAddress;

namespace Company.Domain.Entities
{
    public class Address : DomainEntity
    {
        #region Ctor

        private Address()
        {
            Id = Guid.NewGuid();
        }
        
        private Address(Guid companyId): this()
        {
            Check.NotEmpty(companyId, nameof(companyId));
            CompanyId = companyId;
        }

        #endregion

        #region Properties

        public Guid Id { get; private set; }
        public Guid CompanyId { get; private set; }
        public string Title { get; private set; }
        public CountryRef CountryRef { get; private set; }
        public CityRef CityRef { get; private set; }
        public DistrictRef DistrictRef { get; private set; }
        public string Details { get; private set; }
        public string ZipCode { get; private set; }
        public bool IsPrimary { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime LastModificationTime { get; private set; }
        public Guid LastModifiedUserId { get; private set; }

        #endregion

        #region Methods

        public static Address Create(Guid companyId, string title, CountryRef countryRef, CityRef cityRef, 
            DistrictRef districtRef, string details, string zipCode = null, bool isPrimary = false)
        {
            return new Address(companyId)
                .SetTitle(title)
                .SetCountry(countryRef)
                .SetCity(cityRef)
                .SetDistrict(districtRef)
                .SetDetails(details)
                .SetZipCode(zipCode)
                .SetPrimary(isPrimary);
        }

        public Address SetTitle(string title)
        {
            CheckRule(new AddressTitleRequiredRule(title));
            Title = title;
            OnUpdated();

            return this;
        }

        public Address SetCountry(CountryRef countryRef)
        {
            CheckRule(new AddressMustHaveCountryRule(countryRef));
            CountryRef = countryRef;
            OnUpdated();

            return this;
        }

        public Address SetCity(CityRef cityRef)
        {
            CheckRule(new AddressMustHaveCityRule(cityRef));
            CityRef = cityRef;
            OnUpdated();

            return this;
        }

        public Address SetDistrict(DistrictRef districtRef)
        {
            DistrictRef = districtRef;
            OnUpdated();

            return this;
        }

        public Address SetDetails(string details)
        {
            CheckRule(new AddressDetailsRequiredRule(details));
            Details = details;
            OnUpdated();

            return this;
        }

        public Address SetZipCode(string zipCode)
        {
            ZipCode = zipCode;
            OnUpdated();

            return this;
        }

        public Address SetPrimary(bool primary)
        {
            IsPrimary = primary;
            OnUpdated();

            return this;
        }

        public Address MarkAsDeleted()
        {
            IsDeleted = true;
            OnUpdated();

            return this;
        }

        #endregion

        #region Helper Methods

        private void OnUpdated()
        {
            LastModifiedUserId = Guid.Empty; //TODO
            LastModificationTime = Clock.Now;
        }

        #endregion
    }
}