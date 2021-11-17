using Alien.BLL.Dtos;
using Alien.BLL.Helpers;
using Alien.DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Profiles
{
    public class TalentProfile : Profile
    {
        public TalentProfile()
        {
            CreateMap<TalentEntity, TalentDto>().ReverseMap();
            CreateMap<TalentDto, ICollection<TalentEntity>>()
                .ConvertUsing<TalentDtoToTalentEntityCollectionConverter<TalentDto, TalentEntity>>();
        }
    }
}
