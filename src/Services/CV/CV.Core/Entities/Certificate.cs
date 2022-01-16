using System;
using Career.Domain;

namespace CurriculumVitae.Core.Entities;

public class Certificate: EntityBase, ISoftDeletable
{
    public string Name { get; set; }
    public string Institution { get; set; }
    public DateTime Date { get; set; }
    public bool IsDeleted { get; set; }
}