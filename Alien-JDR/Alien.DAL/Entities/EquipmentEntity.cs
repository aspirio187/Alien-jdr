using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class EquipmentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public UserEntity Owner { get; set; }

        public int UsedById { get; set; }
        public CharacterEntity UsedBy { get; set; }
        // TODO : add le bol 
        public bool IsUsed { get; set; }
    }
}
