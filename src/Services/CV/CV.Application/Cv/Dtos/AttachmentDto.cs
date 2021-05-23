using System;

namespace CurriculumVitae.Application.Cv.Dtos
{
    public class AttachmentDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
    }
}