using Career.Exceptions;

namespace Company.Domain.Refs
{
    public class SectorRef: IdNameRef
    {
        private SectorRef(string refId, string name) : base(refId, name)
        {
        }

        public static SectorRef Create(string id, string name) => new SectorRef(id, name);
    }
}