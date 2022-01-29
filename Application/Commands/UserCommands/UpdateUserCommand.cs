using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string DId;
        public string Name;
        public string ShortFact1;
        public string ShortFact2;
        public string ShortFact3;
        public string AboutMe;
        public string InterestedIn;
        public string Photo;

        public UpdateUserCommand(string dId, string name, string shortFact1,
            string shortFact2, string shortFact3, string aboutMe,
            string interestedIn, string photo)
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
        private readonly UserCRUDService _userService;

        public UpdateUserCommandHandler(
            UserCRUDService userCRUDService)
        {
            _userService = userCRUDService;
        }

        public async Task<bool> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _userService.UpdateUser(request.DId, request.Name,
                    request.ShortFact1, request.ShortFact2, request.ShortFact3,
                    request.AboutMe, request.InterestedIn, request.Photo);
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
