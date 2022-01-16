namespace Career.Domain.Audit;

public interface ICreationAudited: IHasCreationTime
{
    long? CreatorUserId { get; set; } 
}