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

            builder.HasOne(p => p.User) // La on dit qu'il a un user avec plusieur party players, cest pas plutot dire qu'il a un owner avec plusieurs party players ? 
                .WithMany(c => c.PartyPlayers)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            // TODO : A verif ; il faut récup les diff players de la même party, voir si cest bien encodé dans user entity et party entity etc

            builder.HasOne(pp => pp.Party)
                .WithMany(p => p.PartyPlayers)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Character)
                .WithMany(c => c.PartyPlayers)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            // TODO : Add le bool
        }
    }
}
