using Domain.Core.Objects;
using DTOs.Notifications;

namespace Application.Core.Mappers
{
    public static class NotificationAppMappers
    {
        public static ReadNotification FromDomainObjectToApiDto(
            Notification notification)
        {
            return new ReadNotification(
                dId: notification.DId,
                userDId: notification.UserDId,
                type: notification.Type,
                text: notification.Text,
                wasOpen: notification.WasOpen,
                relatedDId: notification.RelatedDId
                );
        }
    }
}
