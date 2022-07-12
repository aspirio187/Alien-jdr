using System;
using System.Collections.Generic;

namespace Alien.DAL.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
