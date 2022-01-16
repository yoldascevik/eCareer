namespace Job.Application.Candidate.Dtos;

public class CandidateInputDto
{
    public Guid UserId { get; set; }
    public Guid CvId { get; set; }
    public string CoverLetter { get; set; }
    public string Channel { get; set; }
    public string Referance { get; set; }
}