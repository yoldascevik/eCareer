using System;

namespace CurriculumVitae.Application.CoverLetter.Dtos;

public class CoverLetterInputDto
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}