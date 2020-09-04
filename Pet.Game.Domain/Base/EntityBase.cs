using System;
using System.ComponentModel.DataAnnotations;

namespace Pet.Game.Domain.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; internal set; }

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

		public void SetId(Guid id)
		{
			this.Id = id;
		}

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
