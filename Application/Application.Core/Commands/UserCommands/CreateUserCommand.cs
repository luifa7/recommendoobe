using Application.Core.Interfaces;
using Application.Core.Services;
using Domain.Core.Objects;
using MediatR;

namespace Application.Core.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string UserName { get; }
        public string Name { get; }
        public string ShortFact1 { get; }
        public string ShortFact2 { get; }
        public string ShortFact3 { get; }
        public string AboutMe { get; }
        public string InterestedIn { get; }
        public string Photo { get; }

        public CreateUserCommand(
            string userName,
            string name,
            string shortFact1,
            string shortFact2,
            string shortFact3,
            string aboutMe,
            string interestedIn,
            string photo)
        {
            UserName = userName;
            Name = name;
            ShortFact1 = shortFact1;
            ShortFact2 = shortFact2;
            ShortFact3 = shortFact3;
            AboutMe = aboutMe;
            InterestedIn = interestedIn;
            Photo = photo;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserCrudService _userService;

        public CreateUserCommandHandler(IUserCrudService userCrudService)
        {
            _userService = userCrudService;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(
                request.UserName,
                request.Name,
                request.ShortFact1,
                request.ShortFact2,
                request.ShortFact3,
                request.AboutMe,
                request.InterestedIn,
                request.Photo);

            await _userService.PersistAsync(user);

            return user;
        }
    }
}
