﻿using Alien.DAL.Entities;
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
            builder.Property(p => p.Id)
                .IsRequired(true);

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

            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(join => join.ToTable("UserRole"));

            builder.HasMany(u => u.LobbyPlayers)
                .WithOne(lb => lb.User);

            builder.HasMany(u => u.SentNotifications)
                .WithOne(n => n.Sender);

            builder.HasMany(u => u.ReceivedNotifications)
                .WithOne(n => n.Receiver);
        }
    }
}
