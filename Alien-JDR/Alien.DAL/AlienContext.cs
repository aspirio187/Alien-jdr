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
        public DbSet<PartyEntity> Parties { get; set; }
        //public DbSet<PartyPlayersEntity> PartyPlayers { get; set; }

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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
