using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string UserName;
        public string Name;
        public string ShortFact1;
        public string ShortFact2;
        public string ShortFact3;
        public string AboutMe;
        public string InterestedIn;
        public string Photo;
        public string[] Friends;

        public CreateUserCommand(string userName, string name,
            string shortFact1, string shortFact2, string shortFact3,
            string aboutMe, string interestedIn, string photo, string[] friends)
        {
            UserName = userName;
            Name = name;
            ShortFact1 = shortFact1;
            ShortFact2 = shortFact2;
            ShortFact3 = shortFact3;
            AboutMe = aboutMe;
            InterestedIn = interestedIn;
            Photo = photo;
            Friends = friends;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly UserCRUDService _userService;

        public CreateUserCommandHandler(UserCRUDService userCRUDService)
        {
            _userService = userCRUDService;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(request.UserName, request.Name,
                request.ShortFact1, request.ShortFact2, request.ShortFact3,
                request.AboutMe, request.InterestedIn, request.Photo,
                request.Friends);

            await _userService.PersistAsync(user);

            return user;
        }
    }
}
