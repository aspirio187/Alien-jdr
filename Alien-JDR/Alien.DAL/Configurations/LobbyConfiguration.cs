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
    public class LobbyConfiguration : IEntityTypeConfiguration<LobbyEntity>
    {
        public void Configure(EntityTypeBuilder<LobbyEntity> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.Mode)
                .IsRequired(true)
                .HasMaxLength(15);

            builder.Property(p => p.Id)
                .IsRequired(true);

            builder.Property(p => p.MaximumPlayers)
                .IsRequired(true);

            builder.Property(x => x.HostIp)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(p => p.PartyPlayers)
                .WithOne(pp => pp.Party);
        }
    }
}
