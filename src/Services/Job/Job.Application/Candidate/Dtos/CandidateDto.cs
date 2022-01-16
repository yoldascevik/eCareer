namespace Job.Application.Candidate.Dtos;

public class CandidateDto
{
    public Guid Id { get; set; }
    public Guid JobId { get; init; }
    public Guid UserId { get; init; }
    public Guid CvId { get; init; }
    public string CoverLetter { get; init; }
    public string Channel { get; init; }
    public string Referance { get; init; }
    public DateTime ApplicationDate { get; init; }
    public DateTime? WithdrawalDate { get; set; }
    public bool IsActive { get; set; }
}