using Alien.DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Profiles
{
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            CreateMap<string, EquipmentEntity>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(
                        src => src));
        }
    }
}
