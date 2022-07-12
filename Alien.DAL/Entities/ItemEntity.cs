using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUsable { get; set; }
        public bool IsFetish { get; set; }

        public int CharacterId { get; set; }
        public CharacterEntity Character { get; set; }
    }
}
