using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Services;
using MediatR;

namespace Application.Core.Commands.FriendCommands
{
    public class DeleteFriendCommand : IRequest<bool>
    {
        public string User1DId { get; }
        public string User2DId { get; }

        public DeleteFriendCommand(string user1DId, string user2DId)
        {
            User1DId = user1DId;
            User2DId = user2DId;
        }
    }

    public class DeleteFriendCommandHandler :
        IRequestHandler<DeleteFriendCommand, bool>
    {
        private readonly FriendCrudService _friendService;

        public DeleteFriendCommandHandler(
            FriendCrudService friendCrudService)
        {
            _friendService = friendCrudService;
        }

        public async Task<bool> Handle(
            DeleteFriendCommand request,
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
