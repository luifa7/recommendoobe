using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface INotificationRepository
    {
        /// <summary>
        /// Return all Notifications by user DId
        /// </summary>
        /// <param name="userDId">DId of the User</param>
        /// <returns></returns>
        List<Notification> GetAllByUserDId(string userDId);

        /// <summary>
        /// Return all not opened Notifications by user DId
        /// </summary>
        /// <param name="userDId">DId of the User</param>
        /// <returns></returns>
        List<Notification> GetAllNotOpenedByUserDId(string userDId);

        /// <summary>
        /// Return all not opened Notifications by user DId
        /// </summary>
        /// <param name="userDId">DId of the User</param>
        /// <returns></returns>
        int GetNotOpenedCountByUserDId(string userDId);

        /// <summary>
        /// Persist Notification into the Database
        /// </summary>
        /// <param name="notification">Notification to persist</param>
        /// <returns></returns>
        Task PersistAsync(Notification notification);

        /// <summary>
        /// Mark Notification as opened
        /// </summary>
        /// <param name="dId">DId of the Notification to update</param>
        /// <returns></returns>
        Task MarkNotificationAsOpened(string dId);

        /// <summary>
        /// Mark Notification as opened
        /// </summary>
        /// <param name="userDId">DId of the User to mark all notifications as read</param>
        /// <returns></returns>
        Task MarkAllNotificationAsOpenedByUserDId(string userDId);

        /// <summary>
        /// Delete Notification from Database
        /// </summary>
        /// <param name="dId">DId of the Notification to delete</param>
        /// <returns></returns>
        Task DeleteNotification(string dId);
    }
}
