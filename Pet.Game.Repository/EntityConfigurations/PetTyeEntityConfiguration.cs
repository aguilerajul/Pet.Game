using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pet.Game.Repository.EntityConfigurations
{    public class PetTyeEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.PetType>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.PetType> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
