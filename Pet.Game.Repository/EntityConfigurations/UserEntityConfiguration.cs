using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pet.Game.Repository.EntityConfigurations
{    public class UserEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasMany(e => e.Pets);
            builder.Ignore(u => u.Pets);
        }
    }
}
