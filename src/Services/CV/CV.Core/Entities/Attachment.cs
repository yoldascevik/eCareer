using System;
using Career.Domain;

namespace CurriculumVitae.Core.Entities
{
    public class Attachment: EntityBase, ISoftDeletable
    {
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}