using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(AlienContext context)
            : base(context)
        {

        }

        public override void Create(UserEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            // Hacher le password
            // Enregistrer l'user
        }
    }
}
