using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class UpdateCityCommand : IRequest<bool>
    {
        public string DId;
        public string Name;
        public string Country;
        public string Photo;
        public bool Visited;

        public UpdateCityCommand(string dId, string name, string country,
            string photo, bool visited)
        {
            DId = dId;
            Name = name;
            Country = country;
            Photo = photo;
            Visited = visited;
        }
    }

    public class UpdateCityCommandHandler :
        IRequestHandler<UpdateCityCommand, bool>
    {
        private readonly CityCRUDService _cityService;

        public UpdateCityCommandHandler(CityCRUDService cityCRUDService)
        {
            _cityService = cityCRUDService;
        }

        public async Task<bool> Handle(UpdateCityCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _cityService.UpdateCity(request.DId, request.Name,
                    request.Country, request.Photo, request.Visited);

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
