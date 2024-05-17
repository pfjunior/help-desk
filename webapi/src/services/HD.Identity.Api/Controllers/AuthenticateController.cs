using HD.Core.Messages.Integration;
using HD.Identity.Api.Models.ViewModels;
using HD.Identity.Api.Services;
using HD.MessageBus;
using HD.WebApi.Core.Controllers;
using HD.WebApi.Core.Identitty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HD.Identity.Api.Controllers;

[Authorize]
[Route("api/identity")]
public class AuthenticateController : MainController
{
    private readonly AuthenticationService _authenticationService;
    private readonly IMessageBus _bus;

    public AuthenticateController(AuthenticationService authenticationService, IMessageBus bus)
    {
        _authenticationService = authenticationService;
        _bus = bus;
    }


    [AllowAnonymous]
    [HttpPost("authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Authenticate(UserLoginViewModel user)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _authenticationService.SignInManager.PasswordSignInAsync(user.Email, user.Password, false, true);

        if (result.Succeeded) return CustomResponse(await _authenticationService.GenerateJwt(user.Email));

        if (result.IsLockedOut)
        {
            AddProcessingError("User temporarily blocked for invalid attempts");
            return CustomResponse();
        }

        AddProcessingError("User or Password incorrects");
        return CustomResponse();
    }

    [ClaimsAuthorize("Administrator", "Admin")]
    [ClaimsAuthorize("Support", "Support")]
    [HttpPost("new-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register(UserRegisterViewModel user)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var newUser = new IdentityUser
        {
            UserName = user.Email,
            Email = user.Email,
            EmailConfirmed = true
        };

        var result = await _authenticationService.UserManager.CreateAsync(newUser, user.Password);

        if (result.Succeeded)
        {
            var userRegisterResult = await UserRegister(user);

            if (!userRegisterResult.ValidationResult.IsValid)
            {
                await _authenticationService.UserManager.DeleteAsync(newUser);
                return CustomResponse(userRegisterResult.ValidationResult);
            }
            return CustomResponse(await _authenticationService.GenerateJwt(user.Email));
        }

        foreach (var error in result.Errors) AddProcessingError(error.Description);

        return CustomResponse();
    }



    private async Task<ResponseMessage> UserRegister(UserRegisterViewModel userRegister)
    {
        var user = await _authenticationService.UserManager.FindByEmailAsync(userRegister.Email);
        var userRegistred = new UserRegistredIntergrationEvent(Guid.Parse(user.Id), userRegister.FirstName, userRegister.LastName, userRegister.Email, userRegister.DepartmentCode, userRegister.DepartmentName);

        try
        {
            return await _bus.RequestAsync<UserRegistredIntergrationEvent, ResponseMessage>(userRegistred);
        }
        catch
        {
            await _authenticationService.UserManager.DeleteAsync(user);
            throw;
        }
    }
}
