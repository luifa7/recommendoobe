using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using Domain.Objects;
using DTOs.Cities;
using DTOs.Recommendations;
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
        private readonly TagCRUDService _tagService;
        private readonly RecommendationCRUDService _recommendationService;
        private readonly NotificationCRUDService _notificationService;

        public UsersController(IMediator mediator,
            UserCRUDService userCRUDService,
            TagCRUDService tagCRUDService,
            RecommendationCRUDService recommendationCRUDService,
            NotificationCRUDService notificationCRUDService)
        {
            _mediator = mediator;
            _userService = userCRUDService;
            _tagService = tagCRUDService;
            _recommendationService = recommendationCRUDService;
            _notificationService = notificationCRUDService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ReadUser> users = new();
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["username"]))
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

        [HttpGet("{dId}/recommendations")]
        public IActionResult GetRecommendationsByUserCreatorDId(string dId)
        {
            var domainRecommendations =
                _recommendationService.GetRecommendationsByUserCreatorDId(dId);
            List<ReadRecommendation> recommendations = new();
            foreach (Recommendation domainRecommendation in domainRecommendations)
            {
                List<Tag> domainTags =
                    _tagService.GetTagsByRecommendationDId(domainRecommendation.DId);
                recommendations.Add(
                RecommendationAppMappers.FromDomainObjectToApiDTO(
                    domainRecommendation, domainTags));
            }
            return Ok(recommendations);
        }

        [HttpGet("{dId}/notifications-count")]
        public IActionResult GetNotOpenedCountByUserDId(string dId)
        {
            return Ok(_notificationService.GetNotOpenedCountByUserDId(dId));
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

        [HttpPut("{dId}")]
        public async Task<IActionResult> Update(string dId,
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
    }
}
