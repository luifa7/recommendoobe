﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class MarkNotificationAsOpenedCommand : IRequest<bool>
    {
        public string DId;

        public MarkNotificationAsOpenedCommand(string dId)
        {
            DId = dId;
        }
    }

    public class MarkNotificationAsOpenedCommandHandler :
        IRequestHandler<MarkNotificationAsOpenedCommand, bool>
    {
        private readonly NotificationCRUDService _notificationService;

        public MarkNotificationAsOpenedCommandHandler(
            NotificationCRUDService notificationServiceCRUDService)
        {
            _notificationService = notificationServiceCRUDService;
        }

        public async Task<bool> Handle(MarkNotificationAsOpenedCommand request,
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
