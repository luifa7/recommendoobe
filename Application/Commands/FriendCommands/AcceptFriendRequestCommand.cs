using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class AcceptFriendRequestCommand : IRequest<bool>
    {
        public string ReceiverDId;
        public string SenderDId;

        public AcceptFriendRequestCommand(string receiverDId, string senderDId)
        {
            ReceiverDId = receiverDId;
            SenderDId = senderDId;
        }
    }

    public class AcceptFriendRequestCommandHandler :
        IRequestHandler<AcceptFriendRequestCommand, bool>
    {
        private readonly FriendCRUDService _friendService;
        private const string FriendshipAccepted = "accepted";

        public AcceptFriendRequestCommandHandler(FriendCRUDService friendCRUDService)
        {
            _friendService = friendCRUDService;
        }

        public async Task<bool> Handle(AcceptFriendRequestCommand request,
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
