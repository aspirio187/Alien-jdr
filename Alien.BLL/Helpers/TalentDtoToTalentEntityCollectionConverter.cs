using Alien.BLL.Dtos;
using Alien.DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Helpers
{
    public class TalentDtoToTalentEntityCollectionConverter<TTalentDto, TTalentEntity> : ITypeConverter<TTalentDto, ICollection<TTalentEntity>>
        where TTalentDto : TalentDto
        where TTalentEntity : TalentEntity
    {
        public ICollection<TTalentEntity> Convert(TTalentDto source, ICollection<TTalentEntity> destination, ResolutionContext context)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (destination is null)
            {
                destination = new List<TTalentEntity>();
            }

            destination.Add(context.Mapper.Map<TTalentEntity>(source));

            return destination;
        }
    }
}
