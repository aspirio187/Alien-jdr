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
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationEntity, NotificationDto>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(
                        src => src.Status.ToString()));
            CreateMap<CreateNotificationDto, NotificationEntity>();
        }
    }
}
