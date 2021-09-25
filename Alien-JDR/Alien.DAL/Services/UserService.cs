﻿using Alien.DAL.Entities;
using Alien.DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Services
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        public UserService(AlienContext context)
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
