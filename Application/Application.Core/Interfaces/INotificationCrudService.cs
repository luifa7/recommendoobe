using Domain.Core.Objects;

namespace Application.Core.Interfaces;

public interface INotificationCrudService
{
    List<Notification> GetAllByUserDId(string dId);
    List<Notification> GetAllNotOpenedByUserDId(string dId);
    int GetNotOpenedCountByUserDId(string dId);
    Task PersistAsync(Notification notification);
    Task MarkNotificationAsOpened(string dId);
    Task MarkAllNotificationAsOpenedByUserDId(string userDId);
    Task DeleteNotification(string dId);
}
