using HD.Domain.Core.Interfaces;
using HD.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace HD.Api.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    protected MainController(INotifier notifier) => _notifier = notifier;

    protected bool ValidOperation() => !_notifier.HasNotification();

    protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
    {
        if (ValidOperation()) return new ObjectResult(result) { StatusCode = Convert.ToInt32(statusCode) };

        return BadRequest(new { errors = _notifier.GetNotifications().Select(n => n.Message) });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyInvalidModelError(modelState);

        return CustomResponse();
    }

    protected void NotifyInvalidModelError(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
        {
            var message = error.Exception == null ? error.ErrorMessage : error.Exception.Message;

            NotifyError(message);
        }
    }

    protected void NotifyError(string message) => _notifier.Handle(new Notification(message));
}
