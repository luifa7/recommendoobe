using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Services;
using Domain.Core.Objects;
using MediatR;

namespace Application.Core.Commands.FriendCommands
{
    public class AcceptFriendRequestCommand : IRequest<bool>
    {
        public string ReceiverDId { get; }
        public string SenderDId { get; }

        public AcceptFriendRequestCommand(string receiverDId, string senderDId)
        {
            ReceiverDId = receiverDId;
            SenderDId = senderDId;
        }
    }

    public class AcceptFriendRequestCommandHandler :
        IRequestHandler<AcceptFriendRequestCommand, bool>
    {
        private readonly FriendCrudService _friendService;
        private const string FriendshipAccepted = "accepted";

        public AcceptFriendRequestCommandHandler(FriendCrudService friendCrudService)
        {
            _friendService = friendCrudService;
        }

        public async Task<bool> Handle(
            AcceptFriendRequestCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var friendshipInTheOtherDirection = Friend.Create(
                request.ReceiverDId, request.SenderDId, FriendshipAccepted);

                await _friendService.AcceptFriendRequest(
                    request.ReceiverDId,
                    request.SenderDId,
                    friendshipInTheOtherDirection);
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
