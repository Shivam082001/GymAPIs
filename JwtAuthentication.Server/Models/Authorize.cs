using JwtAuthentication.Server.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JwtAuthentication.Server.Models;
public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute
{
    private string[] allowedRoles;

    public AuthorizeAttribute(params string[] roles)
    {
        this.allowedRoles = roles;
    }

    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        var user = actionContext.RequestContext.Principal as User;

        if (user == null)
        {
            return false;
        }

        if (allowedRoles.Any() && !allowedRoles.Contains(user.Role))
        {
            return false;
        }

        return base.IsAuthorized(actionContext);
    }
}
