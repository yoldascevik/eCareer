using System;
using System.Collections.Generic;
using System.Linq;
using Career.Domain.Helpers;

namespace Career.Domain.Entities
{
    public abstract class Entity
    {
        protected abstract IEnumerable<object> GetIdentityMembers();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;
            
            var other = obj as Entity;
            if (other == null) return false;
            return GetIdentityMembers().SequenceEqual(other.GetIdentityMembers());
        }

        public static bool operator ==(Entity leftEntity, Entity rightEntity)
        {
            if (ReferenceEquals(leftEntity, rightEntity))
            {
                return true;
            }

            if (((object) leftEntity == null) || ((object) rightEntity == null))
            {
                return false;
            }

            return leftEntity.Equals(rightEntity);
        }

        public static bool operator !=(Entity leftEntity, Entity rightEntity)
        {
            return !(leftEntity == rightEntity);
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(GetIdentityMembers());
        }
    }
}