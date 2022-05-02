using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.Commands;
using Application.Core.Commands.FriendCommands;
using Application.Core.Commands.NotificationCommands;
using Application.Core.Commands.RecommendationCommands;
using Application.Core.Commands.UserCommands;
using Application.Core.Mappers;
using Application.Core.Services;
using Domain.Core.Objects;
using DTOs.Cities;
using DTOs.Friend;
using DTOs.Notifications;
using DTOs.Recommendations;
using DTOs.Users;
using Infrastructure.Core.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Core.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserCrudService _userService;
        private readonly TagCrudService _tagService;
        private readonly RecommendationCrudService _recommendationService;
        private readonly NotificationCrudService _notificationService;
        private readonly FriendCrudService _friendService;

        public UsersController(
            IMediator mediator,
            UserCrudService userCrudService,
            TagCrudService tagCrudService,
            RecommendationCrudService recommendationCrudService,
            NotificationCrudService notificationCrudService,
            FriendCrudService friendCrudService)
        {
            _mediator = mediator;
            _userService = userCrudService;
            _tagService = tagCrudService;
            _recommendationService = recommendationCrudService;
            _notificationService = notificationCrudService;
            _friendService = friendCrudService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string username)
        {
            List<ReadUser> users = new();
            if (!string.IsNullOrEmpty(username))
            {
                var domainUser = _userService.GetByUserNameCaseInsensitive(username);
                if (domainUser != null)
                {
                    users.Add(
                    UserAppMappers.FromDomainObjectToApiDto(domainUser));
                }
            }
            else
            {
                var domainUsers = _userService.GetAll();
                domainUsers.ForEach(dre => users.Add(
                    UserAppMappers.FromDomainObjectToApiDto(dre)));
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
                domainUsers.ForEach(dUser => users.Add(
                    UserAppMappers.FromDomainObjectToApiDto(dUser)));
                return Ok(users);
            }
            else
            {
                var domainUser = _userService.GetByDId(dId);
                if (domainUser != null)
                {
                    ReadUser user = UserAppMappers.FromDomainObjectToApiDto(domainUser);
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
                CityAppMappers.FromDomainObjectToApiDto(dcity)));
            return Ok(cities);
        }

        [HttpGet("{dId}/recommendations")]
        public IActionResult GetRecommendationsByUserCreatorDId(string dId)
        {
            var domainRecommendations =
                _recommendationService.GetRecommendationsByUserCreatorDId(dId);
            List<ReadRecommendation> recommendations = (
                from domainRecommendation in domainRecommendations
                let domainTags = _tagService.GetTagsByRecommendationDId(domainRecommendation.DId)
                select RecommendationAppMappers.FromDomainObjectToApiDto(domainRecommendation, domainTags)).ToList();

            return Ok(recommendations);
        }

        [HttpGet("{dId}/notifications/count")]
        public IActionResult GetNotOpenedCountByUserDId(string dId)
        {
            return Ok(_notificationService.GetNotOpenedCountByUserDId(dId));
        }

        [HttpGet("{dId}/notifications")]
        public IActionResult GetAllNotificationsByUserDId(string dId)
        {
            var domainNotifications =
                _notificationService.GetAllByUserDId(dId);
            List<ReadNotification> notifications = new();
            notifications.AddRange(domainNotifications.Select(
                NotificationAppMappers.FromDomainObjectToApiDto));

            return Ok(notifications);
        }

        [HttpGet("{dId}/notifications/unread")]
        public IActionResult GetAllUnreadNotificationsByUserDId(string dId)
        {
            var domainNotifications =
                _notificationService.GetAllNotOpenedByUserDId(dId);
            List<ReadNotification> notifications = domainNotifications.Select(
                NotificationAppMappers.FromDomainObjectToApiDto).ToList();

            return Ok(notifications);
        }

        [HttpGet("{dId}/friends")]
        public IActionResult GetFriendsByDId(string dId)
        {
            List<string> friends = new();
            var domainFriends = _friendService.GetAllFriendsByUserDId(dId);
            domainFriends.ForEach(dfriend => friends.Add(dfriend.FriendDId));
            return Ok(friends);
        }

        [HttpGet("{dId}/friends/sent")]
        public IActionResult GetSentFriendsByDId(string dId)
        {
            List<string> friends = new();
            var domainFriends = _friendService.GetAllSentPendingByUserDId(dId);
            domainFriends.ForEach(dfriend => friends.Add(dfriend.FriendDId));
            return Ok(friends);
        }

        [HttpGet("{dId}/friends/received")]
        public IActionResult GetReceivedFriendsByDId(string dId)
        {
            List<string> friends = new();

            // this case returns list of UserDIds since received has the id as Friend
            var domainFriends = _friendService.GetAllReceivedPendingByUserDId(dId);
            domainFriends.ForEach(dfriend => friends.Add(dfriend.UserDId));

            return Ok(friends);
        }

        [HttpGet("{dId}/friends/{friendDId}/ispending")]
        public IActionResult IsPending(string dId, string friendDId)
        {
            var pendingExist =
                _friendService.IsRequestPendingBetweenUsers(dId, friendDId);
            return Ok(pendingExist);
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
                createUser.UserName.ToLower(),
                createUser.Name,
                createUser.ShortFact1,
                createUser.ShortFact2,
                createUser.ShortFact3,
                createUser.AboutMe,
                createUser.InterestedIn,
                createUser.Photo
                );
                User user = await _mediator.Send(command);

                return Created(user.DId, UserAppMappers.FromDomainObjectToApiDto(user));
            }
        }

        [HttpPost("{dId}/friends/")]
        public async Task<IActionResult> Create(
            string dId,
            [FromBody] CreateFriend createFriend)
        {
            var command = new CreatePendingFriendCommand(
                dId, createFriend.FriendDId
                );
            Friend friend = await _mediator.Send(command);

            var notificationCommand = new CreateNotificationCommand(
                createFriend.FriendDId,
                Notification.TypeFriendRequestReceived,
                friend.DId
                );
            await _mediator.Send(notificationCommand);

            return Created(friend.UserDId, friend);
        }

        [HttpPut("{dId}")]
        public async Task<IActionResult> Update(
            string dId,
            [FromBody] UpdateUser updateUser)
        {
            var command = new UpdateUserCommand(
                dId,
                updateUser.Name,
                updateUser.ShortFact1,
                updateUser.ShortFact2,
                updateUser.ShortFact3,
                updateUser.AboutMe,
                updateUser.InterestedIn,
                updateUser.Photo);
            bool success = await _mediator.Send(command);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{dId}/notifications")]
        public async Task<IActionResult> MarkAllNotificationsAsOpenedByUserDId(string dId)
        {
            var command = new MarkAllNotificationAsOpenedByUserCommand(dId);
            bool success = await _mediator.Send(command);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{dId}/friends/{friendDId}/accept")]
        public async Task<IActionResult> AcceptFriendRequest(
            string dId, string friendDId)
        {
            var command = new AcceptFriendRequestCommand(dId, friendDId);
            bool success = await _mediator.Send(command);
            if (success)
            {
                var notificationCommand = new CreateNotificationCommand(
                friendDId,
                Notification.TypeFriendRequestAccepted,
                dId
                );
                await _mediator.Send(notificationCommand);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{dId}")]
        public async Task<IActionResult> Delete(string dId)
        {
            var command = new DeleteUserCommand(dId);
            bool success = await _mediator.Send(command);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{dId}/notifications/{notificationDId}")]
        public async Task<IActionResult> DeleteNotification(
            string dId,
            string notificationDId)
        {
            var command = new DeleteRecommendationCommand(notificationDId);
            bool success = await _mediator.Send(command);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{dId}/friends/{friendDId}")]
        public async Task<IActionResult> DeleteFriend(string dId, string friendDId)
        {
            var command = new DeleteFriendCommand(
            dId, friendDId);
            bool success = await _mediator.Send(command);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
