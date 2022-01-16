using Career.Domain;

namespace Job.Domain.JobAggregate.Constants;

public class JobStatus : Enumeration
{
    private JobStatus(int id, string name) : base(id, name) { }

    public static readonly JobStatus Draft = new JobStatus(1, "Draft");
    public static readonly JobStatus WaitingForApproval = new JobStatus(2, "WaitingForApproval");
    public static readonly JobStatus Published = new JobStatus(3, "Published");
    public static readonly JobStatus Expired = new JobStatus(4, "Expired");
    public static readonly JobStatus Revoked = new JobStatus(5, "Revoked");
}