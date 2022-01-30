using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class NotificationRepository: INotificationRepository
    {
        private DBContext _dbContext;

        public NotificationRepository()
        {
            _dbContext = new DBContext();
        }

        public List<Notification> GetAllByUserDId(string userDId)
        {
            var notificationsFromDB = _dbContext.Notifications
                .Where(n => n.UserDId ==userDId).ToList();
            List<Notification> notifications = new();

            notificationsFromDB.ForEach(n => notifications.Add
            (NotificationMappers.FromDBEntityToDomainObject(n)));

            return notifications;

        }

        public List<Notification> GetAllNotOpenedByUserDId(string userDId)
        {
            var notificationsFromDB = _dbContext.Notifications
                .Where(n => (n.UserDId == userDId) && (!n.WasOpen)).ToList();
            List<Notification> notifications = new();

            notificationsFromDB.ForEach(n => notifications.Add
            (NotificationMappers.FromDBEntityToDomainObject(n)));

            return notifications;

        }

        public Task PersistAsync(Notification notification)
        {
            var notificationDBEntity =
                NotificationMappers.FromDomainObjectToDBEntity(notification);
            _dbContext.Notifications.Add(notificationDBEntity);
            return _dbContext.SaveChangesAsync();
        }

        public Task MarkNotificationAsOpened(string dId)
        {
            var notification = _dbContext.Notifications.First(n => n.DId == dId);
            notification.WasOpen = true;
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteNotification(string dId)
        {
            _dbContext.Remove(_dbContext.Notifications.Single(n => n.DId == dId));
            return _dbContext.SaveChangesAsync();
        }
    }
}
