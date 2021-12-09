using Alien.BLL.Dtos;
using Alien.DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Profiles
{
    public class LobbyProfile : Profile
    {
        public LobbyProfile()
        {
            CreateMap<LobbyEntity, LobbyDto>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(
                        src => src.Status == LobbyStatusEnum.Started ? "Commencé"
                                : src.Status == LobbyStatusEnum.Waiting ? "Attente"
                                : src.Status == LobbyStatusEnum.Over ? "Terminé" : string.Empty));
        }
    }
}
