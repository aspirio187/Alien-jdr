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
    class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.Property(p => p.Id)
             .IsRequired(true);

            builder.Property(p => p.UserFromId)
             .IsRequired(true);

            builder.Property(p => p.UserToId)
             .IsRequired(true);

            builder.Property(p => p.PartyId)
             .IsRequired(true);


            builder.HasOne(p => p.UserFrom)
                .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(p => p.UserTo)
                .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.PartyFor)
                .WithMany(c => c.Notifications)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(n => n.SendTime)
                .IsRequired(true);
            // TODO : Add la date ! + le bool
        }

    }
}

