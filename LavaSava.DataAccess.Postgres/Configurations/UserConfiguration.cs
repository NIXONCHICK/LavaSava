using LavaSava.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LavaSava.DataAccess.Postgres.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            // Установка первичного ключа
            builder.HasKey(x => x.Id);

            // Настройка свойств с использованием метода Property
            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50); // Пример ограничения длины имени

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100); // Пример ограничения длины электронной почты

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.Bio)
                .HasMaxLength(255); // Пример ограничения длины биографии

            builder.Property(x => x.Image)
                .HasMaxLength(255); // Пример ограничения длины пути к изображению
            
            builder.HasMany(x => x.Followings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
