using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<CharacterEntity> Characters { get; set; }
        public ICollection<EquipmentEntity> Equipments { get; set; }
        public ICollection<RoleEntity> Roles { get; set; }
        public ICollection<NotificationEntity> Notifications { get; set; }
        public ICollection<PartyPlayersEntity> PartyPlayers { get; set; }
    }
}
