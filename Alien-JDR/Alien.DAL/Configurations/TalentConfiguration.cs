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
    class TalentConfiguration : IEntityTypeConfiguration<TalentEntity>
    {
        public void Configure(EntityTypeBuilder<TalentEntity> builder)
        {
            builder.Property(p => p.Id)
             .IsRequired(true);

            builder.Property(x => x.Name)
             .IsRequired()
             .HasMaxLength(50);

            builder.Property(x => x.Description)
             .IsRequired()
             .HasMaxLength(500);
        }
    }
}
