using Application.Core.Interfaces;
using Application.Core.Services;
using MediatR;

namespace Application.Core.Commands.NotificationCommands
{
    public class DeleteNotificationCommand : IRequest<bool>
    {
        public string DId { get; }

        public DeleteNotificationCommand(string dId)
        {
            DId = dId;
        }
    }

    public class DeleteNotificationCommandHandler :
        IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly INotificationCrudService _notificationService;

        public DeleteNotificationCommandHandler(
            INotificationCrudService notificationCrudService)
        {
            _notificationService = notificationCrudService;
        }

        public async Task<bool> Handle(
            DeleteNotificationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _notificationService.DeleteNotification(request.DId);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
        }
    }
}
