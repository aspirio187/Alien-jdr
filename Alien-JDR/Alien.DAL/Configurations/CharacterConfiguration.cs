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
    public class CharacterConfiguration : IEntityTypeConfiguration<CharacterEntity>
    {
        public void Configure(EntityTypeBuilder<CharacterEntity> builder)
        {
            builder.Property(p => p.OwnerId)
                .IsRequired(true);

            builder.Property(p => p.IdentificationStamp)
                .IsRequired(true);

            builder.HasOne(p => p.Owner)
                .WithMany(c => c.Characters)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.UsedBy)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.PicturePath)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Career)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Race)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Appearance)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Objectives)
                .IsRequired()
                .HasMaxLength(150);
    
            builder.Property(x => x.Friend)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Rival)
                .IsRequired()
                .HasMaxLength(50);
// TALENTS 
            builder.Property(p => p.StressPoints)
                .IsRequired(false);

            builder.Property(p => p.LifePoints)
                .IsRequired(false);

            builder.Property(p => p.RadiationPoints)
                .IsRequired(false);

            builder.Property(p => p.ExperiencePoints)
                .IsRequired(false);

            builder.Property(p => p.StoryPoints)
                .IsRequired(false);

            builder.Property(p => p.Strength)
                .IsRequired(false);

            builder.Property(p => p.CloseCombat)
                .IsRequired(false);

            builder.Property(p => p.Stamina)
                .IsRequired(false);

            builder.Property(p => p.HeavyMachines)
                .IsRequired(false);

            builder.Property(p => p.Agility)
               .IsRequired(false);

            builder.Property(p => p.RangedCombat)
               .IsRequired(false);

            builder.Property(p => p.Mobility)
               .IsRequired(false);

            builder.Property(p => p.Piloting)
               .IsRequired(false);

            builder.Property(p => p.Empathy)
               .IsRequired(false);

            builder.Property(p => p.Commanding)
               .IsRequired(false);

            builder.Property(p => p.Manipulation)
               .IsRequired(false);

            builder.Property(p => p.MedicalCare)
               .IsRequired(false);

            builder.Property(p => p.Mind)
               .IsRequired(false);

            builder.Property(p => p.Observer)
               .IsRequired(false);

            builder.Property(p => p.Survival)
               .IsRequired(false);

            builder.Property(p => p.Contech)
               .IsRequired(false);

// TODO : 2bool
        }
        
    }
}
