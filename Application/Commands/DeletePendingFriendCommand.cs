using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class DeletePendingFriendCommand : IRequest<bool>
    {
        public string User1DId;
        public string User2DId;

        public DeletePendingFriendCommand(string user1DId, string user2DId)
        {
            User1DId = user1DId;
            User2DId = user2DId;
        }
    }

    public class DeletePendingFriendCommandHandler :
        IRequestHandler<DeletePendingFriendCommand, bool>
    {
        private readonly FriendCRUDService _friendService;

        public DeletePendingFriendCommandHandler(
            FriendCRUDService friendCRUDService)
        {
            _friendService = friendCRUDService;
        }

        public async Task<bool> Handle(DeletePendingFriendCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _friendService.DeletePendingFriendRequest(
                request.User1DId, request.User2DId);

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
