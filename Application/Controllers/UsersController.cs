using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using Domain.Objects;
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
            var domainUsers = _userService.GetAll();
            List<ReadUser> users = new();
            domainUsers.ForEach(dre => users.Add(
                UserAppMappers.FromDomainObjectToApiDTO(dre)));
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUser createUser)
        {
            var command = new CreateUserCommand(
                createUser.UserName, createUser.Name, createUser.ShortFact1,
                createUser.ShortFact2, createUser.ShortFact3,
                createUser.AboutMe, createUser.InterestedIn, createUser.Photo,
                createUser.Friends
                );
            User user = await _mediator.Send(command);

            return Created(user.DId, UserAppMappers.FromDomainObjectToApiDTO(user));
        }
    }
}
