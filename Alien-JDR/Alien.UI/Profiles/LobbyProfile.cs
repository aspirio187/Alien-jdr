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
    public class LobbyProfile : Profile
    {
        public LobbyProfile()
        {
            CreateMap<LobbyDto, LobbyModel>();
        }
    }
}
