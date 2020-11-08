using System;

namespace Definition.Application.Education.EducationLevel
{
    [Serializable]
    public class EducationLevelDto
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
    }
}