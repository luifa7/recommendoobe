using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class DeleteNotificationCommand : IRequest<bool>
    {
        public string DId;

        public DeleteNotificationCommand(string dId)
        {
            DId = dId;
        }
    }

    public class DeleteNotificationCommandHandler :
        IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly NotificationCRUDService _notificationService;

        public DeleteNotificationCommandHandler(
            NotificationCRUDService notificationCRUDService)
        {
            _notificationService = notificationCRUDService;
        }

        public async Task<bool> Handle(DeleteNotificationCommand request,
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
