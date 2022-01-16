using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.SocialProfile.Dtos;

public class SocialProfileDto
{
    public string Id { get; set; }
    public IdNameRefDto Type { get; set; }
    public string Username { get; set; }
    public string ProfileUrl { get; set; }
}