using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace HD.WebApi.Core.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(HttpStatusCode? statusCode = HttpStatusCode.OK, object? result = null)
    {
        return ValidOperation()
            ? new ObjectResult(result) { StatusCode = Convert.ToInt32(statusCode) }
            : BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors) AddProcessingError(error.ErrorMessage);

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var erro in validationResult.Errors) AddProcessingError(erro.ErrorMessage);

        return CustomResponse();
    }


    protected bool ValidOperation() => !Errors.Any();

    protected void AddProcessingError(string error) => Errors.Add(error);

    protected void CleanErrors() => Errors.Clear();
}
