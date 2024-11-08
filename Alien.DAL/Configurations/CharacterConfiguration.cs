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
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Talents)
                .WithMany(t => t.Characters)
                .UsingEntity(join => join.ToTable("CharacterTalent"));

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Image)
                .IsRequired();

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

            builder.Property(x => x.Friends)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Rivals)
                .IsRequired()
                .HasMaxLength(150);
 
            builder.Property(p => p.StressPoints)
                .IsRequired(true);

            builder.Property(p => p.LifePoints)
                .IsRequired(true);

            builder.Property(p => p.RadiationPoints)
                .IsRequired(true);

            builder.Property(p => p.ExperiencePoints)
                .IsRequired(true);

            builder.Property(p => p.StoryPoints)
                .IsRequired(true);

            builder.Property(p => p.Strength)
                .IsRequired(true);

            builder.Property(p => p.CloseCombat)
                .IsRequired(true);

            builder.Property(p => p.Stamina)
                .IsRequired(true);

            builder.Property(p => p.HeavyMachines)
                .IsRequired(true);

            builder.Property(p => p.Agility)
               .IsRequired(true);

            builder.Property(p => p.RangedCombat)
               .IsRequired(true);

            builder.Property(p => p.Mobility)
               .IsRequired(true);

            builder.Property(p => p.Piloting)
               .IsRequired(true);

            builder.Property(p => p.Empathy)
               .IsRequired(true);

            builder.Property(p => p.Commandment)
               .IsRequired(true);

            builder.Property(p => p.Manipulation)
               .IsRequired(true);

            builder.Property(p => p.MedicalCare)
               .IsRequired(true);

            builder.Property(p => p.Mind)
               .IsRequired(true);

            builder.Property(p => p.Observation)
               .IsRequired(true);

            builder.Property(p => p.Survival)
               .IsRequired(true);

            builder.Property(p => p.Comtech)
               .IsRequired(true);
        }
    }
}
