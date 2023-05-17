using Application.Core.Interfaces;
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
        private readonly INotificationCrudService _notificationService;

        public MarkNotificationAsOpenedCommandHandler(
            INotificationCrudService notificationServiceCrudService)
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
