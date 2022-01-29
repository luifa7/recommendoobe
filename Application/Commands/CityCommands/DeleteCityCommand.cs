using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class DeleteCityCommand : IRequest<bool>
    {
        public string DId;

        public DeleteCityCommand(string dId)
        {
            DId = dId;
        }
    }

    public class DeleteCityCommandHandler :
        IRequestHandler<DeleteCityCommand, bool>
    {
        private readonly CityCRUDService _cityService;

        public DeleteCityCommandHandler(CityCRUDService cityCRUDService)
        {
            _cityService = cityCRUDService;
        }

        public async Task<bool> Handle(DeleteCityCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _cityService.DeleteCity(request.DId);

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
