using Application.Core.Services;
using MediatR;

namespace Application.Core.Commands.NotificationCommands
{
    public class MarkNotificationAsOpenedCommand : IRequest<bool>
    {
        public string DId { get; }

        public MarkNotificationAsOpenedCommand(string dId)
        {
            DId = dId;
        }
    }

    public class MarkNotificationAsOpenedCommandHandler :
        IRequestHandler<MarkNotificationAsOpenedCommand, bool>
    {
        private readonly NotificationCrudService _notificationService;

        public MarkNotificationAsOpenedCommandHandler(
            NotificationCrudService notificationServiceCrudService)
        {
            _notificationService = notificationServiceCrudService;
        }

        public async Task<bool> Handle(
            MarkNotificationAsOpenedCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _notificationService.MarkNotificationAsOpened(request.DId);

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
