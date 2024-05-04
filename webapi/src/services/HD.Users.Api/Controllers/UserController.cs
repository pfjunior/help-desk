using HD.Users.Api.Domain.Interfaces;
using HD.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HD.Users.Api.Controllers;


//[Authorize]
[Route("api/user")]
public class UserController : MainController
{
    // TODO: Implementar claims para o usuário

    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(null, await _repository.GetAll());
    }
}
