using Domain.Objects;
using DTOs.Notifications;

namespace Infrastructure.Mappers
{
    public class NotificationAppMappers
    {
        public static ReadNotification FromDomainObjectToApiDTO(
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
