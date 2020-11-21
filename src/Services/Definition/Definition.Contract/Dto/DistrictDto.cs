using System;

namespace Definition.Contract.Dto
{
    [Serializable]
    public class DistrictDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CityId { get; set; }
        public string CityCode { get; set; }
        public string CountryId { get; set; }
        public string CountryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}