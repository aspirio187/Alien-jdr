using Alien.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Password)
                .IsRequired();

            builder.HasCheckConstraint("CK_username", "LEN(Username) > 5");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Firstname)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Lastname)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.HasMany(u => u.Characters)
                .WithOne(u => u.Owner);
        }
    }
}
