namespace Job.Application.Job.Dtos;

public class CandidateSummaryDto
{
    public Guid CandidateId { get; set; }
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
}