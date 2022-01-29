using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class DeleteFriendCommand : IRequest<bool>
    {
        public string User1DId;
        public string User2DId;

        public DeleteFriendCommand(string user1DId, string user2DId)
        {
            User1DId = user1DId;
            User2DId = user2DId;
        }
    }

    public class DeleteFriendCommandHandler :
        IRequestHandler<DeleteFriendCommand, bool>
    {
        private readonly FriendCRUDService _friendService;

        public DeleteFriendCommandHandler(
            FriendCRUDService friendCRUDService)
        {
            _friendService = friendCRUDService;
        }

        public async Task<bool> Handle(DeleteFriendCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _friendService.DeleteFriend(
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
