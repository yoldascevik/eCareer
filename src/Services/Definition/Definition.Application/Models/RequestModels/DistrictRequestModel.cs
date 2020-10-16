﻿using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Models.RequestModels
{
    public class DistrictRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CityId { get; set; }

        public string Latitude { get; set; }
        
        public string Longitude { get; set; }
    }
}