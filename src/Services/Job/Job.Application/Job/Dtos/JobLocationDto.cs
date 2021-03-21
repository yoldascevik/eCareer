using System;

namespace Job.Application.Job.Dtos
{
    public class JobLocationDto
    {
        public Guid Id { get; set; }
        public string CountryId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
    }
}