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
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
             .IsRequired()
             .HasMaxLength(50);

            builder.HasMany(r => r.Users)
                .WithMany(u => u.Roles);
        }
    }
}
