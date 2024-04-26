using HD.Users.Api.Domain.Entities;
using HD.Users.Api.Domain.Interfaces;
using HD.Users.Api.Dtos;
using HD.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HD.Users.Api.Controllers;


//[Authorize]
[Route("api/user")]
public class UserController : MainController
{
    // TODO: Implementar a comunicação por fila
    // TODO: Implementar claims para o usuário

    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(await _repository.GetAll());
    }

    [HttpPost("new-user")]
    public async Task<IActionResult> Register(CreateUserDto user)
    {
        await _repository.AddAsync(new User(user.FirstName, user.LastName, user.Email));

        await _repository.UnitOfWork.CommitAsync();

        return CustomResponse();
    }
}
