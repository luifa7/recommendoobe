using System.Threading;
using System.Threading.Tasks;
using Application.Core.Services;
using Domain.Core.Objects;
using MediatR;

namespace Application.Core.Commands.FriendCommands
{
    public class CreatePendingFriendCommand : IRequest<Friend>
    {
        public string UserDId { get; }
        public string FriendDId { get; }

        public CreatePendingFriendCommand(string userDId, string friendDId)
        {
            UserDId = userDId;
            FriendDId = friendDId;
        }
    }

    public class CreatePendingFriendCommandHandler :
        IRequestHandler<CreatePendingFriendCommand, Friend>
    {
        private readonly FriendCrudService _friendService;
        private const string FriendshipPending = "pending";

        public CreatePendingFriendCommandHandler(FriendCrudService friendCrudService)
        {
            _friendService = friendCrudService;
        }

        public async Task<Friend> Handle(
            CreatePendingFriendCommand request,
            CancellationToken cancellationToken)
        {
            var friend = Friend.Create(
                request.UserDId, request.FriendDId, FriendshipPending);

            await _friendService.PersistAsync(friend);

            return friend;
        }
    }
}
