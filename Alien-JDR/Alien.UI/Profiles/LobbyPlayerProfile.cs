using Alien.BLL.Dtos;
using Alien.UI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Profiles
{
    public class LobbyPlayerProfile : Profile
    {
        public LobbyPlayerProfile()
        {
            // TODO : Modifier Status et Type et voir s'ils sont nécessaire
            CreateMap<LobbyPlayerDto, LobbyPlayerModel>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(
                        src => "Prêt"))
                .ForMember(
                    dest => dest.PlayerName,
                    opt => opt.MapFrom(
                        src => src.User.Username))
                .ForMember(
                    dest => dest.CharacterName,
                    opt => opt.MapFrom(
                        src => src.Character.Name))
                .ForMember(
                    dest => dest.PlayerType,
                    opt => opt.MapFrom(
                        src => "PJ"));

            CreateMap<LobbyPlayerArrival, LobbyPlayerModel>().ReverseMap();

        }
    }
}
