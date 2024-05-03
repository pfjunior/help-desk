﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace HD.WebApi.Core.Identitty;

public class CustomAuthorization
{
    public static bool UserClaimValidate(HttpContext context, string claimName, string claimValue) =>
        context.User.Identity.IsAuthenticated && context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilterRequirement)) => Arguments = new object[] { new Claim(claimName, claimValue) };
}

public class ClaimFilterRequirement : IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimFilterRequirement(Claim claim) => _claim = claim;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (!CustomAuthorization.UserClaimValidate(context.HttpContext, _claim.Type, _claim.Value))
            context.Result = new StatusCodeResult(403);
    }
}
