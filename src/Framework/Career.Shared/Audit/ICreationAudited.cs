namespace Career.Shared.Audit
{
    public interface ICreationAudited: IHasCreationTime
    {
        long? CreatorUserId { get; set; } 
    }
}