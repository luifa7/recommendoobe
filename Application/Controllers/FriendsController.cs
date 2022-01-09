using System;
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
            List<string> friends = new();
            if (String.IsNullOrEmpty(HttpContext.Request.Query["status"]))
            {
                var domainFriends = _friendService.GetAllFriendsByUserDId(userDId);
                domainFriends.ForEach(dfriend => friends.Add(dfriend.FriendDId));
            }
            else if (HttpContext.Request.Query["status"] == "sent")
            {
                var domainFriends = _friendService.GetAllSentPendingByUserDId(userDId);
                domainFriends.ForEach(dfriend => friends.Add(dfriend.FriendDId));
            }
            else if (HttpContext.Request.Query["status"] == "received")
            {
                // this case returns list of UserDIds since received has the id as Friend
                var domainFriends = _friendService.GetAllReceivedPendingByUserDId(userDId);
                domainFriends.ForEach(dfriend => friends.Add(dfriend.UserDId));
            }

            return Ok(friends);
        }

        [HttpGet("{userDId}/ispending")]
        public IActionResult IsPending(string userDId)
        {
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["userid"]))
            {
                var pendingExist =
                    _friendService.IsRequestPendingBetweenUsers(
                        userDId, HttpContext.Request.Query["userid"]);
                return Ok(pendingExist);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFriend createFriend)
        {
            var command = new CreatePendingFriendCommand(
                createFriend.UserDId, createFriend.FriendDId
                );
            Friend friend = await _mediator.Send(command);

            return Created(friend.UserDId, friend);
        }
    }
}
