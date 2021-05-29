using System;
using System.Collections.Generic;
using CurriculumVitae.Core.Constants;

namespace CurriculumVitae.Core.Entities
{
    public class PersonalInfo
    {
        public PersonalInfo()
        {
            Disabilities = new List<Disability>();
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; } = GenderType.Unspecified;
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public ICollection<Disability> Disabilities { get; set; }
    }
}