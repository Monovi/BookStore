using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Models.Bases
{
    public abstract partial class BaseEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }


        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        public override int GetHashCode()
        {
            if (Id.Equals(default(int)))
                return base.GetHashCode();

            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity x,BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x,BaseEntity y)
        {
            return !(x == y);
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            Type otherType = other.GetUnproxiedType();
            Type thisType = GetUnproxiedType();
            return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
        }
    }
}
