using HD.Users.Api.Domain.Interfaces;
using HD.WebApi.Core.Controllers;
using HD.WebApi.Core.Identitty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HD.Users.Api.Controllers;


[Authorize]
[Route("api/user")]
public class UserController : MainController
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [ClaimsAuthorize("Adminstrator", "Admin")]
    [ClaimsAuthorize("Support", "Support")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(await _repository.GetAll());
    }
}
