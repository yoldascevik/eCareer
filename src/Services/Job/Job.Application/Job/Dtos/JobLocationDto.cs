using System;

namespace Job.Application.Job.Dtos
{
    public class JobLocationDto
    {
        public Guid Id { get; set; }
        public IdNameRefDto CountryRef { get; set; }
        public IdNameRefDto CityRef { get; set; }
    }
}