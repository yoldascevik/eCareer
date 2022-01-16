using Career.Domain;

namespace CurriculumVitae.Core.Entities;

public class Attachment: EntityBase, ISoftDeletable
{
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string MimeType { get; set; }
    public string Description { get; set; }
    public string SourceUrl { get; set; }
    public Guid UploadedUserId { get; set; }
    public DateTime UploadDate { get; set; }
    public bool IsDeleted { get; set; }
}