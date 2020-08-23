using System;
using System.ComponentModel.DataAnnotations;

namespace Pet.Game.Domain.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; }

        public DateTime Created { get; private set; }

        public DateTime LastModified { get; set; }

        [Required]
		[MaxLength(150)]
        public string Name { get; private set; }

        protected EntityBase() { }

        protected EntityBase(string name): this()
        {
            this.Created = DateTime.UtcNow;
            this.LastModified = DateTime.UtcNow;			
            this.Name = name;
        }

		public override bool Equals(object obj)
		{
			if (!(obj is EntityBase other))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (Id == Guid.Empty || other.Id == Guid.Empty)
				return false;

			if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(other.Name))
				return false;

			if (GetOriginalType() != other.GetOriginalType())
				return false;

			return this.Id == other.Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Name);
		}

		public static bool operator ==(EntityBase entityA, EntityBase entityB)
		{
			if (entityA is null && entityB is null)
				return true;

			if (entityA is null || entityB is null)
				return false;

			return entityA.Equals(entityB);
		}

		public static bool operator !=(EntityBase entityA, EntityBase entityB)
		{
			return !(entityA == entityB);
		}

		private Type GetOriginalType()
		{
			Type type = GetType();
			if (type.ToString().Contains("Castle.Proxies."))
				return type.BaseType;

			return type;
		}
	}
}
