using LavaSava.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LavaSava.DataAccess.Postgres.Configurations
{
public class FollowingConfiguration : IEntityTypeConfiguration<FollowingEntity>
{
    public void Configure(EntityTypeBuilder<FollowingEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.UserId);
    }
}
}