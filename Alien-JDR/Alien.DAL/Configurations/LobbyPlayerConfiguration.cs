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
    public class LobbyPlayerConfiguration : IEntityTypeConfiguration<LobbyPlayerEntity>
    {
        public void Configure(EntityTypeBuilder<LobbyPlayerEntity> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired(true);

            builder.Property(p => p.UserId)
                .IsRequired(true);

            builder.Property(p => p.PartyId)
                .IsRequired(true);

            builder.Property(p => p.CharacterId)
                .IsRequired(false);

            builder.HasOne(p => p.User)
                .WithMany(c => c.LobbyPlayers)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pp => pp.Party)
                .WithMany(p => p.PartyPlayers)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Character)
                .WithMany(c => c.LobbyPlayers)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            // TODO : Add le bool
        }
    }
}
