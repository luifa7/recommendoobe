using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Services;
using MediatR;

namespace Application.Core.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string DId { get; }

        public DeleteUserCommand(string dId)
        {
            DId = dId;
        }
    }

    public class DeleteUserCommandHandler :
        IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly UserCrudService _userService;

        public DeleteUserCommandHandler(
            UserCrudService userCrudService)
        {
            _userService = userCrudService;
        }

        public async Task<bool> Handle(
            DeleteUserCommand request,
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
