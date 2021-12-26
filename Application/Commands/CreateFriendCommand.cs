using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class CreateFriendCommand : IRequest<Friend>
    {
        public string UserDId;
        public string FriendDId;

        public CreateFriendCommand(string userDId, string friendDId)
        {
            UserDId = userDId;
            FriendDId = friendDId;
        }
    }

    public class CreateFriendCommandHandler : IRequestHandler<CreateFriendCommand, Friend>
    {
        private readonly FriendCRUDService _friendService;

        public CreateFriendCommandHandler(FriendCRUDService friendCRUDService)
        {
            _friendService = friendCRUDService;
        }

        public async Task<Friend> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = Friend.Create(request.UserDId, request.FriendDId);

            await _friendService.PersistAsync(friend);

            return friend;
        }
    }
}
