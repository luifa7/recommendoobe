using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class CreateCityCommand : IRequest<City>
    {
        public string Name;
        public string Country;
        public string Photo;
        public string UserDId;
        public bool Visited;

        public CreateCityCommand(string name, string country, string photo,
            string userDId, bool visited)
        {
            Name = name;
            Country = country;
            Photo = photo;
            UserDId = userDId;
            Visited = visited;
        }
    }

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, City>
    {
        private readonly CityCRUDService _cityService;

        public CreateCityCommandHandler(CityCRUDService cityCRUDService)
        {
            _cityService = cityCRUDService;
        }

        public async Task<City> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = City.Create(request.Name, request.Country,
                request.Photo, request.UserDId, request.Visited);

            await _cityService.PersistAsync(city);

            return city;
        }
    }
}
