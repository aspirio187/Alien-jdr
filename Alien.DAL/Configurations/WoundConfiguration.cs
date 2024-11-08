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
    class WoundConfiguration : IEntityTypeConfiguration<WoundEntity>
    {
        public void Configure(EntityTypeBuilder<WoundEntity> builder)
        
        {
            builder.Property(p => p.Id)
                .IsRequired(true);

            builder.Property(p => p.CharacterId)
                .IsRequired(true);

            builder.HasOne(w => w.Character)
                .WithMany(c => c.MajorWounds)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(500);

        }
    }
}
