using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HD.WebApi.Core.User;

public interface IAspNetUser
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    string GetUserToken();
    bool IsAutenticate();
    bool HasRole(string role);
    IEnumerable<Claim> GetClaims();
    HttpContext GetHttpContext();
}


public class AspNetUser : IAspNetUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor) => _accessor = accessor;


    public string Name => _accessor.HttpContext.User.Identity.Name;

    public Guid GetUserId() => IsAutenticate() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;

    public string GetUserEmail() => IsAutenticate() ? _accessor.HttpContext.User.GetUserEmail() : "";

    public string GetUserToken() => IsAutenticate() ? _accessor.HttpContext.User.GetUserToken() : "";

    public bool IsAutenticate() => _accessor.HttpContext.User.Identity.IsAuthenticated;

    public bool HasRole(string role) => _accessor.HttpContext.User.IsInRole(role);

    public IEnumerable<Claim> GetClaims() => _accessor.HttpContext.User.Claims;

    public HttpContext GetHttpContext() => _accessor.HttpContext;
}
