using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class CharacterEntity
    {
        public int Id { get; set; }

        //public UserEntity Owner { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
