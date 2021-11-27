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
             .IsRequired(false);

            builder.Property(p => p.UserToId)
             .IsRequired(false);

            builder.Property(p => p.PartyId)
             .IsRequired(false);


            builder.HasOne(p => p.UserFrom)
                .WithMany(u => u.SentNotifications)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(p => p.UserTo)
                .WithMany(u => u.ReceivedNotifications)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Party)
                .WithMany(c => c.Notifications)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(n => n.SendTime)
                .IsRequired(true);
            // TODO : Add la date ! + le bool
        }

    }
}

