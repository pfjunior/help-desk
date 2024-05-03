using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HD.WebApi.Core.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(object? result = null)
    {
        return ValidOperation()
            ? Ok(result)
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


    protected bool ValidOperation() => !Errors.Any();

    protected void AddProcessingError(string error) => Errors.Add(error);

    protected void CleanErrors() => Errors.Clear();
}
