using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;
using Infrastructure.Database;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DbContext _dbContext;

        public NotificationRepository()
        {
            _dbContext = new DbContext();
        }

        public List<Notification> GetAllByUserDId(string userDId)
        {
            var notificationsFromDb = _dbContext.Notifications
                .Where(n => n.UserDId == userDId).ToList();
            List<Notification> notifications = new();

            notificationsFromDb.ForEach(n => notifications.Add(
            NotificationMappers.FromDbEntityToDomainObject(n)));

            return notifications;
        }

        public List<Notification> GetAllNotOpenedByUserDId(string userDId)
        {
            var notificationsFromDb = _dbContext.Notifications
                .Where(n => (n.UserDId == userDId) && (!n.WasOpen)).ToList();
            List<Notification> notifications = new();

            notificationsFromDb.ForEach(n => notifications.Add(
            NotificationMappers.FromDbEntityToDomainObject(n)));

            return notifications;
        }

        public int GetNotOpenedCountByUserDId(string userDId)
        {
            return _dbContext.Notifications
                .Count(n => (n.UserDId == userDId) && (!n.WasOpen));
        }

        public Task PersistAsync(Notification notification)
        {
            var notificationDbEntity =
                NotificationMappers.FromDomainObjectToDbEntity(notification);
            _dbContext.Notifications.Add(notificationDbEntity);
            return _dbContext.SaveChangesAsync();
        }

        public Task MarkNotificationAsOpened(string dId)
        {
            var notification = _dbContext.Notifications.First(n => n.DId == dId);
            notification.WasOpen = true;
            return _dbContext.SaveChangesAsync();
        }

        public Task MarkAllNotificationAsOpenedByUserDId(string userDId)
        {
            var notificationsFromDb = _dbContext.Notifications
                .Where(n => n.UserDId == userDId &&
!n.WasOpen).ToList();

            notificationsFromDb.ForEach(n => n.WasOpen = true);
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteNotification(string dId)
        {
            _dbContext.Remove(_dbContext.Notifications.Single(n => n.DId == dId));
            return _dbContext.SaveChangesAsync();
        }
    }
}
