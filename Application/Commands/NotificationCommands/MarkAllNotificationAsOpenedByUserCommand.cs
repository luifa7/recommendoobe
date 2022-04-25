using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands.NotificationCommands
{
    public class MarkAllNotificationAsOpenedByUserCommand : IRequest<bool>
    {
        public string UserDId { get; }

        public MarkAllNotificationAsOpenedByUserCommand(string userDId)
        {
            UserDId = userDId;
        }
    }

    public class MarkAllNotificationAsOpenedByUserCommandHandler :
        IRequestHandler<MarkAllNotificationAsOpenedByUserCommand, bool>
    {
        private readonly NotificationCrudService _notificationService;

        public MarkAllNotificationAsOpenedByUserCommandHandler(
            NotificationCrudService notificationServiceCrudService)
        {
            _notificationService = notificationServiceCrudService;
        }

        public async Task<bool> Handle(
            MarkAllNotificationAsOpenedByUserCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _notificationService.
                    MarkAllNotificationAsOpenedByUserDId(request.UserDId);

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
