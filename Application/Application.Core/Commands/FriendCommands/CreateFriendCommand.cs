using Application.Core.Interfaces;
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
        private readonly IFriendCrudService _friendService;
        private const string FriendshipPending = "pending";

        public CreatePendingFriendCommandHandler(IFriendCrudService friendCrudService)
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
