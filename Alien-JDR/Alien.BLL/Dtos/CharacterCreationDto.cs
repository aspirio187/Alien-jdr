using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Dtos
{
    public class CharacterCreationDto
    {
        public string Name { get; set; }
        public string Career { get; set; }
        public string Race { get; set; }
        public string Appearance { get; set; }
        public string Objectives { get; set; }
        public List<ItemCreationDto> Items { get; set; }
        public string Friends { get; set; }
        public string Rivals { get; set; }
        public TalentDto Talent { get; set; }

        [IntegerValidator(MinValue = 2, MaxValue = 8, ExcludeRange = false)]
        public int Strength { get; set; }
        public int CloseCombat { get; set; }
        public int Stamina { get; set; }
        public int HeavyMachines { get; set; }

        [IntegerValidator(MinValue = 2, MaxValue = 8, ExcludeRange = false)]
        public int Agility { get; set; }
        public int RangedCombat { get; set; }
        public int Mobility { get; set; }
        public int Piloting { get; set; }

        [IntegerValidator(MinValue = 2, MaxValue = 8, ExcludeRange = false)]
        public int Empathy { get; set; }
        public int Commanding { get; set; }
        public int Manipulation { get; set; }
        public int MedicalCare { get; set; }

        [IntegerValidator(MinValue = 2, MaxValue = 8, ExcludeRange = false)]
        public int Mind { get; set; }
        public int Observer { get; set; }
        public int Survival { get; set; }
        public int Contech { get; set; }

        public Guid OwnerId { get; set; }

        public bool IsPublic { get; set; }
        public Guid IdentificationStamp { get; set; }
    }
}
