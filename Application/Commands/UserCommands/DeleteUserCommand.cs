using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string DId;

        public DeleteUserCommand(string dId)
        {
            DId = dId;
        }
    }

    public class DeleteUserCommandHandler :
        IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly UserCRUDService _userService;

        public DeleteUserCommandHandler(
            UserCRUDService userCRUDService)
        {
            _userService = userCRUDService;
        }

        public async Task<bool> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteUser(request.DId);

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
