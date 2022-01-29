using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class CreatePendingFriendCommand : IRequest<Friend>
    {
        public string UserDId;
        public string FriendDId;

        public CreatePendingFriendCommand(string userDId, string friendDId)
        {
            UserDId = userDId;
            FriendDId = friendDId;
        }
    }

    public class CreatePendingFriendCommandHandler :
        IRequestHandler<CreatePendingFriendCommand, Friend>
    {
        private readonly FriendCRUDService _friendService;
        private const string FriendshipPending = "pending";

        public CreatePendingFriendCommandHandler(FriendCRUDService friendCRUDService)
        {
            _friendService = friendCRUDService;
        }

        public async Task<Friend> Handle(CreatePendingFriendCommand request,
            CancellationToken cancellationToken)
        {
            var friend = Friend.Create(
                request.UserDId, request.FriendDId, FriendshipPending);

            await _friendService.PersistAsync(friend);

            return friend;
        }
    }
}
