namespace Company.Domain.Refs
{
    public class DistrictRef: IdNameRef
    {
        private DistrictRef(string refId, string name) : base(refId, name)
        {
        }

        public static DistrictRef Create(string id, string name) => new DistrictRef(id, name);
    }
}