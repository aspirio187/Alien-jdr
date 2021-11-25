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
    public class PartyConfiguration : IEntityTypeConfiguration<PartyEntity>
    {
        public void Configure(EntityTypeBuilder<PartyEntity> builder)
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

// TODO : ADD les bools

            //builder.HasOne(x => x.Creator);

            //builder.HasMany(x => x.PartyPlayers)
            //    .WithOne(x => x.Party);        .WithMany x players ?          
        }
    }
}
