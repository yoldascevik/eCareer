using Career.Domain;

namespace CurriculumVitae.Core.Entities;

public class CoverLetter : EntityBase, ISoftDeletable
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }
}