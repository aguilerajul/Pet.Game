using System;
using System.ComponentModel.DataAnnotations;

namespace Pet.Game.Domain.Base
{
    public abstract class EntityBase
    {
        private Guid _id;
        public Guid Id => _id;

        private DateTime _created;
        public DateTime Created => _created;

        public DateTime LastModified { get; set; }

        [Required]
        string Name { get; }

        protected EntityBase() { }

        protected EntityBase(string name): this()
        {
            this._id = Guid.NewGuid();
            this._created = DateTime.UtcNow;
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
			if (type.ToString().Contains("Pet.Game."))
				return type.BaseType;

			return type;
		}
	}
}
