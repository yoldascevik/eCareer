namespace Career.Domain.Audit
{
    public interface IModificationAudited: IHasModificationTime
    {
        long? LastModifiedUserId { get; set; } 
    }
}