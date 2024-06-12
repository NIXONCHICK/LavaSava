using LavaSava.DataAccess.Postgres.Configurations;
using LavaSava.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace LavaSava.DataAccess.Postgres
{
  public class LavaSavaDbContext(DbContextOptions<LavaSavaDbContext> options)
  : DbContext(options)
  {
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<FollowingEntity> Followings { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new FollowingConfiguration());
      base.OnModelCreating(modelBuilder);
    }
  }
}
