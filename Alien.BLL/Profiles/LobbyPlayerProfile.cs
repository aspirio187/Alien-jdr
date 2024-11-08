﻿using Alien.BLL.Dtos;
using Alien.DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Profiles
{
    public class LobbyPlayerProfile : Profile
    {
        public LobbyPlayerProfile()
        {
            CreateMap<LobbyPlayerEntity, LobbyPlayerDto>();
            CreateMap<CreateLobbyPlayerDto, LobbyPlayerEntity>();
        }
    }
}
