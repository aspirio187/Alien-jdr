using Alien.DAL.Configurations;
using Alien.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL
{
    public class AlienContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LobbyEntity> Lobbies { get; set; }
        public DbSet<CharacterEntity> Characters { get; set; }
        public DbSet<LobbyPlayerEntity> LobbyPlayers { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<EquipmentEntity> Equipments { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<TalentEntity> Talents { get; set; }
        public DbSet<WoundEntity> Wounds { get; set; }

        public AlienContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
                                Database=Alien-JDR-DB;
                                Integrated Security=True;
                                Connect Timeout=60;");
            //optionsBuilder.UseSqlServer(
            //     @"Server=IDEAPAD-SOULTAN\SQLEXPRESS;Database=Alien-JDR-DB;User Id=thinkpad;
            //                                    Password=soultan187;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CharacterConfiguration());
            modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new LobbyConfiguration());
            modelBuilder.ApplyConfiguration(new LobbyPlayerConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TalentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WoundConfiguration());
        }
    }
}
