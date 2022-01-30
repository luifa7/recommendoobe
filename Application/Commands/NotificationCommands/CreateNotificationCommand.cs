using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class CreateNotificationCommand : IRequest<Notification>
    {
        public string UserDId;
        public string Type;
        public string RelatedDId;

        public CreateNotificationCommand(string userDId, string type,
            string relatedDId)
        {
            UserDId = userDId;
            Type = type;
            RelatedDId = relatedDId;
        }
    }

    public class CreateNotificationCommandHandler :
        IRequestHandler<CreateNotificationCommand, Notification>
    {
        private readonly NotificationCRUDService _notificationService;

        public CreateNotificationCommandHandler(
            NotificationCRUDService notificationCRUDService)
        {
            _notificationService = notificationCRUDService;
        }

        public async Task<Notification> Handle(
            CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = Notification.Create(request.UserDId,
                request.Type, false, request.RelatedDId);

            await _notificationService.PersistAsync(notification);

            return notification;
        }
    }
}
