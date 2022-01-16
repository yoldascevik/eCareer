using Career.Domain;

namespace CurriculumVitae.Core.Entities;

public class DisabilityType : EntityBase, ISoftDeletable
{
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}