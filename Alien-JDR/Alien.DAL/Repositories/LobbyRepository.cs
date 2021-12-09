using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Repositories
{
    public class LobbyRepository : RepositoryBase<LobbyEntity, int>, ILobbyRepository
    {
        public LobbyRepository(AlienContext context) 
            : base(context)
        {
            
        }
    }
}
