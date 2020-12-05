namespace Career.Domain.Entities
{
    public class Entity<TKey> : Entity, IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
    
    public class Entity: IEntity
    {
        
    }
}