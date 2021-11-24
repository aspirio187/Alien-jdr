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
    public class CharacterConfiguration : IEntityTypeConfiguration<CharacterEntity>
    {
        public void Configure(EntityTypeBuilder<CharacterEntity> builder)
        {
            builder.Property(p => p.OwnerId)
                .IsRequired(true);

            builder.Property(p => p.IdentificationStamp)
                .IsRequired(true);

            builder.HasOne(p => p.Owner)
                .WithMany(c => c.Characters)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.UsedBy)
                .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
