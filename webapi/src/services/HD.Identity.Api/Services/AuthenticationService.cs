using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HD.Identity.Api.Data;
using HD.Identity.Api.Extensions;
using HD.Identity.Api.Models;
using HD.WebApi.Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;

namespace HD.Identity.Api.Services;

public class AuthenticationService
{
    public readonly SignInManager<IdentityUser> SignInManager;
    public readonly UserManager<IdentityUser> UserManager;
    private readonly AppTokenSettings _appTokenSettings;
    private readonly ApplicationContext _context;

    private readonly IAspNetUser _aspNetUser;
    private readonly IJwtService _jwtService;

    public AuthenticationService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppTokenSettings> appTokenSettings, ApplicationContext context, IAspNetUser aspNetUser, IJwtService jwtService)
    {
        SignInManager = signInManager;
        UserManager = userManager;
        _appTokenSettings = appTokenSettings.Value;
        _context = context;
        _aspNetUser = aspNetUser;
        _jwtService = jwtService;
    }

    public async Task<ResponseLogin> GenerateJwt(string email)
    {
        var user = await UserManager.FindByEmailAsync(email);
        var claims = await UserManager.GetClaimsAsync(user);

        var identityClaims = await GetClaimsUser(claims, user);
        var encodedToken = await EncodeToken(identityClaims);

        var refreshToken = await GenerateRefreshToken(email);

        return GetResponseToken(encodedToken, user, claims, refreshToken);
    }

    public async Task<RefreshToken> GetRefreshToken(Guid refreshToken)
    {
        var token = await _context.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(u => u.Token == refreshToken);

        return token != null && token.ExpirationDate.ToLocalTime() > DateTime.Now ? token : null;
    }


    private async Task<ClaimsIdentity> GetClaimsUser(ICollection<Claim> claims, IdentityUser user)
    {
        var userRoles = await UserManager.GetRolesAsync(user);

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

    private async Task<string> EncodeToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var currentIssuer = $"{_aspNetUser.GetHttpContext().Request.Scheme}://{_aspNetUser.GetHttpContext().Request.Host}";
        var key = await _jwtService.GetCurrentSigningCredentials();
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = currentIssuer,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = key
        });

        return tokenHandler.WriteToken(token);
    }

    private ResponseLogin GetResponseToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims, RefreshToken refreshToken)
    {
        return new ResponseLogin
        {
            AccessToken = encodedToken,
            RefreshToken = refreshToken.Token,
            ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - DateTimeOffset.UnixEpoch).TotalSeconds);

    private async Task<RefreshToken> GenerateRefreshToken(string email)
    {
        var refreshToken = new RefreshToken
        {
            Username = email,
            ExpirationDate = DateTime.UtcNow.AddHours(_appTokenSettings.RefreshTokenExpiration)
        };

        _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(u => u.Username == email));
        await _context.RefreshTokens.AddAsync(refreshToken);

        await _context.SaveChangesAsync();

        return refreshToken;
    }
}