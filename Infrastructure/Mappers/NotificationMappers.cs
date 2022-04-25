using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public static class NotificationMappers
    {
        public static Notifications FromDomainObjectToDbEntity(
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

        public static Notification FromDbEntityToDomainObject(
            Notifications notificationDbEntity)
        {
            return new Notification(
                dId: notificationDbEntity.DId,
                userDId: notificationDbEntity.UserDId,
                type: notificationDbEntity.Type,
                text: notificationDbEntity.Text,
                wasOpen: notificationDbEntity.WasOpen,
                relatedDId: notificationDbEntity.RelatedDId
                );
        }
    }
}
