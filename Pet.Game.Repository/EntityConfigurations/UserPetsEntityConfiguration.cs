using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pet.Game.Repository.EntityConfigurations
{    public class UserPetsEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.UserPets>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.UserPets> builder)
        {
            builder.ToTable("UserPets", "dbo");
            builder.HasKey(x => x.Id);
            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey("UserId");
            builder.HasOne(e => e.Pet)
               .WithMany()
               .HasForeignKey("PetId");
        }
    }
}
