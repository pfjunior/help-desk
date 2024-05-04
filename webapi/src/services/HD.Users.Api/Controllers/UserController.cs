using HD.Users.Api.Domain.Interfaces;
using HD.WebApi.Core.Controllers;
using HD.WebApi.Core.Identitty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(HttpStatusCode.OK, await _repository.GetAll());
    }
}
