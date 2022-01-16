namespace CurriculumVitae.Application.Attachment.Dtos;

public class AttachmentInputDto
{
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string MimeType { get; set; }
    public string Description { get; set; }
    public string SourceUrl { get; set; }
}