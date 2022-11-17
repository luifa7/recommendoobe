using Application.Core.Services;
using Domain.Core.Objects;
using MediatR;

namespace Application.Core.Commands.CityCommands
{
    public class CreateCityCommand : IRequest<City>
    {
        public string Name { get; }
        public string Country { get; }
        public string Photo { get; }
        public string UserDId { get; }
        public bool Visited { get; }

        public CreateCityCommand(
            string name,
            string country,
            string photo,
            string userDId,
            bool visited)
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
        private readonly CityCrudService _cityService;

        public CreateCityCommandHandler(CityCrudService cityCrudService)
        {
            _cityService = cityCrudService;
        }

        public async Task<City> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = City.Create(
                request.Name,
                request.Country,
                request.Photo,
                request.UserDId,
                request.Visited);

            await _cityService.PersistAsync(city);

            return city;
        }
    }
}
