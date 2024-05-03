using HD.Identity.Api.Models;
using HD.Identity.Api.Models.ViewModels;
using HD.WebApi.Core.Controllers;
using HD.WebApi.Core.Identitty;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HD.Identity.Api.Controllers;

[Route("api/identity")]
public class AuthenticateController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;

    public AuthenticateController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult> Login(UserLoginViewModel user)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, true);

        if (result.Succeeded) return CustomResponse(null, await GenerateJwt(user.Email));

        if (result.IsLockedOut)
        {
            AddProcessingError("User temporarily blocked for invalid attempts");
            return CustomResponse();
        }

        AddProcessingError("User or Password incorrects");
        return CustomResponse();
    }

    [HttpPost("new-account")]
    public async Task<ActionResult> Register(UserRegisterViewModel user)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var newUser = new IdentityUser
        {
            UserName = user.Email,
            Email = user.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(newUser, user.Password);

        if (result.Succeeded) return CustomResponse(null, await GenerateJwt(user.Email));

        foreach (var error in result.Errors) AddProcessingError(error.Description);

        return CustomResponse();
    }

    #region Private Methods
    private async Task<ResponseLogin> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);

        var identityClaims = await GetClaimsUser(claims, user);
        var encodedToken = EncodeToken(identityClaims);

        return GetResponseToken(encodedToken, user, claims);
    }

    private async Task<ClaimsIdentity> GetClaimsUser(ICollection<Claim> claims, IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles) claims.Add(new Claim("role", userRole));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

    private string EncodeToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireHour),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    private ResponseLogin GetResponseToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
    {
        return new ResponseLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpireHour).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - DateTimeOffset.UnixEpoch).TotalSeconds);
    #endregion
}
