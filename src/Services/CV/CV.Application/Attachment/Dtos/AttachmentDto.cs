using System;

namespace CurriculumVitae.Application.Attachment.Dtos
{
    public class AttachmentDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string MimeType { get; set; }
        public string Description { get; set; }
        public string SourceUrl { get; set; }
        public Guid UploadedUserId { get; set; }
        public DateTime UploadDate { get; set; }
    }
}