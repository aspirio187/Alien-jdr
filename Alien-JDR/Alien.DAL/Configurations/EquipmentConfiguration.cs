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
    class EquipmentConfiguration : IEntityTypeConfiguration<EquipmentEntity>
    {
            public void Configure(EntityTypeBuilder<EquipmentEntity> builder)
            {
                builder.Property(p => p.Id)       
                 .IsRequired(true);
            
                builder.Property(p => p.OwnerId)
                 .IsRequired(true);

             builder.Property(x => x.Name)
                 .IsRequired()
                 .HasMaxLength(50);

            builder.Property(x => x.Description)
                 .IsRequired()
                 .HasMaxLength(500);

            builder.HasOne(p => p.Owner)
                 .WithMany(c => c.Equipments)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.UsedBy)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}