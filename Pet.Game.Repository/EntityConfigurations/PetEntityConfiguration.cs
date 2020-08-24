using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pet.Game.Repository.EntityConfigurations
{    public class PetEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Pet>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Pet> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
