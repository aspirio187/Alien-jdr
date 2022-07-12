using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Repositories
{
    public class EquipmentRepository : RepositoryBase<EquipmentEntity, int>, IEquipmentRepository
    {
        public EquipmentRepository(AlienContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<EquipmentEntity>> GetUserEquipmentsAsync(int userId)
        {
            return (await _context.Characters.FindAsync(userId)).Equipments.ToList();
        }
    }
}