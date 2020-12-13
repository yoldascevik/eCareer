namespace Career.Domain.Entities
{
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
    
    public abstract class Entity: IEntity
    {
        
    }
}