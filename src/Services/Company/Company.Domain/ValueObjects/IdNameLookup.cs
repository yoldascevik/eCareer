using Career.Domain;
using Career.Exceptions;

namespace Company.Domain.ValueObjects
{
    public class IdNameLookup: ValueObject
    {
        public string Id { get; }
        public string Name { get; }
        
        public static IdNameLookup Create(string id, string name)
        {
            return new IdNameLookup(id, name);
        }

        private IdNameLookup(string id, string name)
        {
            Check.NotNullOrEmpty(id, nameof(id));
            Check.NotNullOrEmpty(name, nameof(name));

            Id = id;
            Name = name;
        }
    }
}