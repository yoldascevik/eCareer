namespace CurriculumVitae.Application.CoverLetter.Dtos;

public class CoverLetterDto
{
    public string Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}