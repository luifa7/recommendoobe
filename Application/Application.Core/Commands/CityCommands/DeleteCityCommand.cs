using Application.Core.Services;
using MediatR;

namespace Application.Core.Commands.CityCommands
{
    public class DeleteCityCommand : IRequest<bool>
    {
        public string DId { get; }

        public DeleteCityCommand(string dId)
        {
            DId = dId;
        }
    }

    public class DeleteCityCommandHandler :
        IRequestHandler<DeleteCityCommand, bool>
    {
        private readonly CityCrudService _cityService;

        public DeleteCityCommandHandler(CityCrudService cityCrudService)
        {
            _cityService = cityCrudService;
        }

        public async Task<bool> Handle(
            DeleteCityCommand request,
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
