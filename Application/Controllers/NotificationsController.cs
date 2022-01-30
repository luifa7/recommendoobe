using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using Domain.Objects;
using DTOs.Notifications;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class NotificationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly NotificationCRUDService _notificationService;

        public NotificationsController(IMediator mediator,
            NotificationCRUDService notificationCRUDService)
        {
            _mediator = mediator;
            _notificationService = notificationCRUDService;
        }

        [HttpGet("{userDId}")]
        public IActionResult GetAllByUserDId(string userDId)
        {
            List<ReadNotification> notifications = new();
            if (string.IsNullOrEmpty(HttpContext.Request.Query["opened"]))
            {
                var domainNotifications =
                    _notificationService.GetAllByUserDId(userDId);
                foreach (Notification domainNotification in domainNotifications)
                {
                    notifications.Add(
                    NotificationAppMappers.FromDomainObjectToApiDTO(
                        domainNotification));
                }
            }
            else if (HttpContext.Request.Query["opened"] == "false")
            {
                var domainNotifications =
                    _notificationService.GetAllNotOpenedByUserDId(userDId);
                foreach (Notification domainNotification in domainNotifications)
                {
                    notifications.Add(
                    NotificationAppMappers.FromDomainObjectToApiDTO(
                        domainNotification));
                }
            }

            return Ok(notifications);
        }

        [HttpPut("{dId}")]
        public async Task<IActionResult> MarkAsOpened(string dId)
        {
            if (
                (!string.IsNullOrEmpty(HttpContext.Request.Query["opened"]))
                && (HttpContext.Request.Query["opened"] == "true"))
            {
                var command = new MarkNotificationAsOpenedCommand(dId);
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
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{dId}")]
        public async Task<IActionResult> Delete(string dId)
        {
            var command = new DeleteRecommendationCommand(dId);
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
