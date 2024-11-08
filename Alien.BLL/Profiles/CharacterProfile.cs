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
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<CharacterEntity, CharacterMiniatureDto>();
            CreateMap<CharacterCreationDto, CharacterEntity>()
                .ForMember(
                    dest => dest.Talents,
                    opt => opt.Ignore());

            CreateMap<CharacterEntity, CharacterLobbyDto>();
                
        }
    }
}
