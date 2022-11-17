using Application.Core.Services;
using Domain.Core.Objects;
using MediatR;

namespace Application.Core.Commands.NotificationCommands
{
    public class CreateNotificationCommand : IRequest<Notification>
    {
        public string UserDId { get; }
        public string Type { get; }
        public string RelatedDId { get; }

        public CreateNotificationCommand(
            string userDId,
            string type,
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
        private readonly NotificationCrudService _notificationService;

        public CreateNotificationCommandHandler(
            NotificationCrudService notificationCrudService)
        {
            _notificationService = notificationCrudService;
        }

        public async Task<Notification> Handle(
            CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = Notification.Create(
                request.UserDId,
                request.Type,
                wasOpen: false,
                request.RelatedDId);

            await _notificationService.PersistAsync(notification);

            return notification;
        }
    }
}
