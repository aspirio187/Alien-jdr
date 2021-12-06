using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class CharacterEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Career { get; set; }
        public string Race { get; set; }
        public string Appearance { get; set; }
        public string Objectives { get; set; }

        public ICollection<ItemEntity> Items { get; set; }
        public string Friends { get; set; }
        public string Rivals { get; set; }

        public int StressPoints { get; set; }
        public int LifePoints { get; set; } = 10;
        public int RadiationPoints { get; set; }
        public int ExperiencePoints { get; set; }
        public int StoryPoints { get; set; }
        public ICollection<TalentEntity> Talents { get; set; }
        // Sur base de la carrière
        public ICollection<WoundEntity> MajorWounds { get; set; }

        public int Strength { get; set; }
        public int CloseCombat { get; set; }
        public int Stamina { get; set; }
        public int HeavyMachines { get; set; }

        public int Agility { get; set; }
        public int RangedCombat { get; set; }
        public int Mobility { get; set; }
        public int Piloting { get; set; }

        public int Empathy { get; set; }
        public int Commandment { get; set; }
        public int Manipulation { get; set; }
        public int MedicalCare { get; set; }

        public int Mind { get; set; }
        public int Observation { get; set; }
        public int Survival { get; set; }
        public int Comtech { get; set; }

        public ICollection<EquipmentEntity> Equipments { get; set; }

        public Guid OwnerId { get; set; }
        public UserEntity Owner { get; set; }

        public Guid UsedById { get; set; }
        public UserEntity UsedBy { get; set; }

        public bool IsEditable { get; set; }
        public bool IsPublic { get; set; }
        public Guid IdentificationStamp { get; set; }

        public CharacterEntity()
        {

        }

        public CharacterEntity(CharacterEntity character)
        {
            if (character is null) throw new ArgumentNullException(nameof(character));

            Name = character.Name;
            Image = character.Image;
            Career = character.Career;
            Race = character.Race;
            Appearance = character.Appearance;
            Objectives = character.Objectives;
            Items = character.Items;
            Friends = character.Friends;
            Rivals = character.Rivals;
            StressPoints = character.StressPoints;
            LifePoints = character.LifePoints;
            RadiationPoints = character.RadiationPoints;
            StoryPoints = character.StoryPoints;
            Talents = character.Talents;
            MajorWounds = character.MajorWounds;
            Strength = character.Strength;
            CloseCombat = character.CloseCombat;
            Stamina = character.Stamina;
            HeavyMachines = character.HeavyMachines;
            Agility = character.Agility;
            RangedCombat = character.RangedCombat;
            Mobility = character.Mobility;
            Piloting = character.Piloting;
            Empathy = character.Empathy;
            Commandment = character.Commandment;
            Manipulation = character.Manipulation;
            MedicalCare = character.MedicalCare;
            Mind = character.Mind;
            Observation = character.Observation;
            Survival = character.Survival;
            Comtech = character.Comtech;
            Equipments = character.Equipments;
            OwnerId = character.OwnerId;
            UsedBy = character.UsedBy;
            IsEditable = character.IsEditable;
            IsPublic = character.IsPublic;
            IdentificationStamp = character.IdentificationStamp;
        }
    }
}
