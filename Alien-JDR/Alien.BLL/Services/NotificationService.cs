﻿using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository ??
                throw new ArgumentNullException(nameof(notificationRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationAsync(Guid userId)
        {
            IEnumerable<NotificationEntity> notifications = await _notificationRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<NotificationDto>>(notifications.Where(u => u.UserToId == userId));
        }
    }
}
