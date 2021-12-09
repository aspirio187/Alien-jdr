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

            builder.Property(p => p.SenderId)
             .IsRequired(true);

            builder.Property(p => p.ReceiverId)
             .IsRequired(true);

            builder.Property(p => p.LobbyId)
             .IsRequired(true);


            builder.HasOne(p => p.Sender)
                .WithMany(u => u.SentNotifications)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(p => p.Receiver)
                .WithMany(u => u.ReceivedNotifications)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Lobby)
                .WithMany(c => c.Notifications)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(n => n.SentTime)
                .IsRequired(true);
            // TODO : Add la date ! + le bool
        }

    }
}

