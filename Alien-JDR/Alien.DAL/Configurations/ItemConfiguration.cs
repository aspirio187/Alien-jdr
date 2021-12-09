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
    class ItemConfiguration : IEntityTypeConfiguration<ItemEntity>
    {
        public void Configure(EntityTypeBuilder<ItemEntity> builder)
        {
            builder.Property(p => p.Id)
             .IsRequired(true);

            builder.Property(p => p.CharacterId)
             .IsRequired(true);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(i => i.Character)
                .WithMany(c => c.Items)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(i => i.IsFetish)
                .IsRequired(true);
        }

    }
}