namespace Career.Shared.Audit
{
    public interface IModificationAudited: IHasModificationTime
    {
        long? LastModifierUserId { get; set; } 
    }
}