using System;

namespace Definition.Application.Dtos
{
    [Serializable]
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string CityCode { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}