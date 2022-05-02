using System;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly NotificationCrudService _notificationService;

        public DeleteNotificationCommandHandler(
            NotificationCrudService notificationCrudService)
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
