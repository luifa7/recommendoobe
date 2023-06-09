﻿using Application.Core.Interfaces;
using Domain.Core.Interfaces;
using Domain.Core.Objects;

namespace Application.Core.Services
{
    public class NotificationCrudService: INotificationCrudService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationCrudService(INotificationRepository notificationRepository)
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

        public int GetNotOpenedCountByUserDId(string dId)
        {
            return _notificationRepository.GetNotOpenedCountByUserDId(dId);
        }

        public Task PersistAsync(Notification notification)
        {
            return _notificationRepository.PersistAsync(notification);
        }

        public Task MarkNotificationAsOpened(string dId)
        {
            return _notificationRepository.MarkNotificationAsOpened(dId);
        }

        public Task MarkAllNotificationAsOpenedByUserDId(string userDId)
        {
            return _notificationRepository.MarkAllNotificationAsOpenedByUserDId(userDId);
        }

        public Task DeleteNotification(string dId)
        {
            return _notificationRepository.DeleteNotification(dId);
        }
    }
}
