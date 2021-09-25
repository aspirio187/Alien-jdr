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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserSignUpDto, UserEntity>();
            CreateMap<UserSignInDto, UserEntity>();
        }
    }
}
