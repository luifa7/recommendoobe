using Application.Core.Services;
using MediatR;

namespace Application.Core.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string DId { get; }
        public string Name { get; }
        public string ShortFact1 { get; }
        public string ShortFact2 { get; }
        public string ShortFact3 { get; }
        public string AboutMe { get; }
        public string InterestedIn { get; }
        public string Photo { get; }

        public UpdateUserCommand(
            string dId,
            string name,
            string shortFact1,
            string shortFact2,
            string shortFact3,
            string aboutMe,
            string interestedIn,
            string photo)
        {
            DId = dId;
            Name = name;
            ShortFact1 = shortFact1;
            ShortFact2 = shortFact2;
            ShortFact3 = shortFact3;
            AboutMe = aboutMe;
            InterestedIn = interestedIn;
            Photo = photo;
        }
    }

    public class UpdateUserCommandHandler :
        IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly UserCrudService _userService;

        public UpdateUserCommandHandler(
            UserCrudService userCrudService)
        {
            _userService = userCrudService;
        }

        public async Task<bool> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _userService.UpdateUser(
                    request.DId,
                    request.Name,
                    request.ShortFact1,
                    request.ShortFact2,
                    request.ShortFact3,
                    request.AboutMe,
                    request.InterestedIn,
                    request.Photo);
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
