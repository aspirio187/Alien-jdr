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
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemCreationDto, ItemEntity>()
                .ForMember(
                    dest => dest.IsUsable,
                    opt => opt.MapFrom(
                        src => src.IsFetish));
        }
    }
}
