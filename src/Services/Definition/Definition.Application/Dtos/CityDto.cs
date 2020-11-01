using System;

namespace Definition.Application.Dtos
{
    [Serializable]
    public class CityDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
    }
}