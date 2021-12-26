using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using Domain.Objects;
using DTOs.Friends;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class FriendsController: Controller
    {
        private readonly IMediator _mediator;
        private readonly FriendCRUDService _friendService;

        public FriendsController(IMediator mediator, FriendCRUDService friendCRUDService)
        {
            _mediator = mediator;
            _friendService = friendCRUDService;
        }


        [HttpGet("{userDId}")]
        public IActionResult GetByDId(string userDId)
        {
            var domainFriends = _friendService.GetFriendsByUserDID(userDId);
            List<string> friends = new();
            domainFriends.ForEach(dfriend => friends.Add(dfriend.FriendDId));
            return Ok(friends);
            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFriend createFriend)
        {
            var command = new CreateFriendCommand(
                createFriend.UserDId, createFriend.FriendDId
                );
            Friend friend = await _mediator.Send(command);

            return Created(friend.UserDId, friend);
        }
    }
}
