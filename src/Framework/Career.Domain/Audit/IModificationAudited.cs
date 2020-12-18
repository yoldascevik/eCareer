namespace Career.Domain.Audit
{
    public interface IModificationAudited: IHasModificationTime
    {
        long? LastModifierUserId { get; set; } 
    }
}