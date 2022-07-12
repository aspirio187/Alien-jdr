using Alien.BLL.Dtos;
using Alien.UI.Helpers;
using Alien.UI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationDto, NotificationModel>()
                .ForMember(
                    dest => dest.NotificationStatus,
                    opt => opt.MapFrom(
                        src => src.Status.Equals("Accepted") ? NotificationStatusEnum.Accepted
                                : src.Status.Equals("Pending")
                                ? NotificationStatusEnum.Pending : NotificationStatusEnum.Denied))
                .ForMember(
                    dest => dest.Mode,
                    opt => opt.MapFrom(
                        src => src.Lobby.Mode))
                .ForMember(
                    dest => dest.LobbyHost,
                    opt => opt.MapFrom(
                        src => src.SenderName))
                .ForMember(
                    dest => dest.LobbyName,
                    opt => opt.MapFrom(
                        src => src.Lobby.Name))
                .ForMember(
                    dest => dest.SentAt,
                    opt => opt.MapFrom(
                        src => src.SentTime))
                .ForMember(
                    dest => dest.HostId,
                    opt => opt.MapFrom(
                        src => src.SenderId));
        }
    }
}
