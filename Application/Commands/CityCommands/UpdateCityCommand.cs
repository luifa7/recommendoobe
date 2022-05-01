﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Services;
using MediatR;

namespace Application.Core.Commands.CityCommands
{
    public class UpdateCityCommand : IRequest<bool>
    {
        public string DId { get; }
        public string Name { get; }
        public string Country { get; }
        public string Photo { get; }
        public bool Visited { get; }

        public UpdateCityCommand(
            string dId,
            string name,
            string country,
            string photo,
            bool visited)
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
        private readonly CityCrudService _cityService;

        public UpdateCityCommandHandler(CityCrudService cityCrudService)
        {
            _cityService = cityCrudService;
        }

        public async Task<bool> Handle(
            UpdateCityCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _cityService.UpdateCity(
                    request.DId,
                    request.Name,
                    request.Country,
                    request.Photo,
                    request.Visited);

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
