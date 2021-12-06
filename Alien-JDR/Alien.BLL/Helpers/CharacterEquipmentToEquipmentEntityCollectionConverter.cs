using Alien.DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Helpers
{
    public class CharacterEquipmentToEquipmentEntityCollectionConverter<TEquipment, TEquipmentEntity> : ITypeConverter<TEquipment, ICollection<TEquipmentEntity>>
        where TEquipment : IEquatable<string>
        where TEquipmentEntity : EquipmentEntity
    {
        public ICollection<TEquipmentEntity> Convert(List<TEquipment> source, ICollection<TEquipmentEntity> destination, ResolutionContext context)
        {
            return null;
        }

        public ICollection<TEquipmentEntity> Convert(TEquipment source, ICollection<TEquipmentEntity> destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
