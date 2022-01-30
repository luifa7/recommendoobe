using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public class NotificationMappers
    {
        public static Notifications FromDomainObjectToDBEntity(
            Notification notification)
        {
            return new Notifications()
            {
                DId = notification.DId,
                UserDId = notification.UserDId,
                Type = notification.Type,
                Text = notification.Text,
                WasOpen = notification.WasOpen,
                RelatedDId = notification.RelatedDId
        };

        }

        public static Notification FromDBEntityToDomainObject(
            Notifications notificationDBEntity)
        {
            return new Notification(
                dId: notificationDBEntity.DId,
                userDId: notificationDBEntity.UserDId,
                type: notificationDBEntity.Type,
                text: notificationDBEntity.Text,
                wasOpen: notificationDBEntity.WasOpen,
                relatedDId: notificationDBEntity.RelatedDId
                );
        }
    }
}
