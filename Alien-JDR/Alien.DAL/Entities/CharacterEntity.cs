using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public enum CharacterTypeEnum
    {
        Human,
        Android
    }

    public class CharacterEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string PicturePath { get; set; }
        public string Carreer { get; set; }
        public string Race { get; set; }
        public string Appearance { get; set; }
        public string Objectives { get; set; }

        public string FetishItem { get; set; }
        public string LittleItems { get; set; }
        public string Friend { get; set; }
        public string Rival { get; set; }

        public int StressPoints { get; set; }
        public int LifePoints { get; set; }
        public int RadiationPoints { get; set; }
        public int ExperiencePoints { get; set; }
        public int StoryPoints { get; set; }
        public ICollection<TalentEntity> Talents { get; set; }
        public ICollection<WoundEntity> MajorWounds { get; set; }

        [IntegerValidator(MinValue = 2, MaxValue = 5, ExcludeRange = false)]
        public int Strength { get; set; }
        public int CloseCombat { get; set; }
        public int Stamina { get; set; }
        public int HeavyMachines { get; set; }

        [IntegerValidator(MinValue = 2, MaxValue = 5, ExcludeRange = false)]
        public int Agility { get; set; }
        public int RangedCombat { get; set; }
        public int Mobility { get; set; }
        public int Piloting { get; set; }

        [IntegerValidator(MinValue = 2, MaxValue = 5, ExcludeRange = false)]
        public int Empathy { get; set; }
        public int Commanding { get; set; }
        public int Manipulation { get; set; }
        public int MedicalCare { get; set; }

        [IntegerValidator(MinValue = 2, MaxValue = 5, ExcludeRange = false)]
        public int Mind { get; set; }
        public int Observer { get; set; }
        public int Survival { get; set; }
        public int Contech { get; set; }

        public ICollection<EquipmentEntity> Equipments { get; set; }

        public Guid OwnerId { get; set; }
        public UserEntity Owner { get; set; }

        public Guid UsedById { get; set; }
        public UserEntity UsedBy { get; set; }

        public bool IsEditable { get; set; }
        public bool IsPublic { get; set; }
        public Guid IdentificationStamp { get; set; }
    }
}
