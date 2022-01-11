using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using Domain.Objects;
using DTOs.Cities;
using DTOs.Users;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class UsersController: Controller
    {
        private readonly IMediator _mediator;
        private readonly UserCRUDService _userService;

        public UsersController(IMediator mediator, UserCRUDService userCRUDService)
        {
            _mediator = mediator;
            _userService = userCRUDService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ReadUser> users = new();
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["username"]))
            {
                var username = HttpContext.Request.Query["username"];

                var domainUser = _userService.GetByUserNameCaseInsensitive(username);
                if (domainUser != null)
                {
                    users.Add(
                    UserAppMappers.FromDomainObjectToApiDTO(domainUser));
                }
            }
            else
            {
                var domainUsers = _userService.GetAll();
                domainUsers.ForEach(dre => users.Add(
                    UserAppMappers.FromDomainObjectToApiDTO(dre)));
                
            }
            return Ok(users);
        }

        [HttpGet("{dId}")]
        public IActionResult GetByDId(string dId)
        {
            if (dId.Contains(','))
            {
                string[] usersDIds = dId.Split(',');
                var domainUsers = _userService.GetUsersByDIdList(usersDIds);
                List<ReadUser> users = new();
                domainUsers.ForEach(duser => users.Add(
                    UserAppMappers.FromDomainObjectToApiDTO(duser)));
                return Ok(users);
            }
            else
            {
                var domainUser = _userService.GetByDId(dId);
                if (domainUser != null)
                {
                    ReadUser user = UserAppMappers.FromDomainObjectToApiDTO(domainUser);
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("{dId}/cities")]
        public IActionResult GetCitiesByUserDId(string dId)
        {
            var domainCities = _userService.GetCitiesByUserDId(dId);
            List<ReadCity> cities = new();
            domainCities.ForEach(dcity => cities.Add(
                CityAppMappers.FromDomainObjectToApiDTO(dcity)));
            return Ok(cities);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUser createUser)
        {
            var domainUserWithThisUsername =
                _userService.GetByUserNameCaseInsensitive(
                createUser.UserName);
            if (domainUserWithThisUsername != null)
            {
                return Conflict();
            }
            else
            {
                var command = new CreateUserCommand(
                createUser.UserName.ToLower(), createUser.Name, createUser.ShortFact1,
                createUser.ShortFact2, createUser.ShortFact3,
                createUser.AboutMe, createUser.InterestedIn, createUser.Photo
                );
                User user = await _mediator.Send(command);

                return Created(user.DId, UserAppMappers.FromDomainObjectToApiDTO(user));

            }
        }
    }
}
