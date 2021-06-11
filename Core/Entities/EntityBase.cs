using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public abstract class EntityBase : IEquatable<object>
    {
        [Key]
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string LastChangeBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastChangeOn { get; set; }
        public string Comment { get; set; }

        protected EntityBase()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                EntityBase other = (EntityBase)obj;
                return this.Id == other.Id;
            }
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
