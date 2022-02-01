﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;

namespace Application.Services
{
    public class NotificationCRUDService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationCRUDService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public List<Notification> GetAllByUserDId(string dId)
        {
            return _notificationRepository.GetAllByUserDId(dId);
        }

        public List<Notification> GetAllNotOpenedByUserDId(string dId)
        {
            return _notificationRepository.GetAllNotOpenedByUserDId(dId);
        }

        public Task PersistAsync(Notification notification)
        {
            return _notificationRepository.PersistAsync(notification);
        }

        public Task MarkNotificationAsOpened(string dId)
        {
            return _notificationRepository.MarkNotificationAsOpened(dId);
        }

        public Task DeleteNotification(string dId)
        {
            return _notificationRepository.DeleteNotification(dId);
        }
    }
}