using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.AddLocation
{
    public class AddLocationCommand: ICommand<JobLocationDto>
    {
        public AddLocationCommand(Guid jobId, string countryId, string cityId, string districtId)
        {
            JobId = jobId;
            CountryId = countryId;
            CityId = cityId;
            DistrictId = districtId;
        }

        public Guid JobId { get; }
        public string CountryId { get; }
        public string CityId { get; }
        public string DistrictId { get; }
    }
}