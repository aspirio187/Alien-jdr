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
    class EquipmentConfiguration : IEntityTypeConfiguration<EquipmentEntity>
    {
            public void Configure(EntityTypeBuilder<EquipmentEntity> builder)
            {
                builder.Property(p => p.Id)       
                 .IsRequired(true);
            

             builder.Property(x => x.Name)
                 .IsRequired()
                 .HasMaxLength(50);

            builder.HasOne(e => e.Character)
                .WithMany(c => c.Equipments)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            // TODO : ADD le bool
        }

    }
}
