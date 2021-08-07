using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWTAuthentication.Authentication
{
    public class CustomClaimsAuthoriz
    {
    }
    public class CustomClaimsAuthorizAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string claimType;
        private string claimValue;
        public CustomClaimsAuthorizAttribute(string claimType,
           string claimValue)
        {
            this.claimType = claimType;
            this.claimValue = claimValue;
        }
        public void OnAuthorization(AuthorizationFilterContext
           filterContext)
        {
            var CurrentUser = filterContext.HttpContext.User as ClaimsPrincipal;
            var Username = CurrentUser.Identity.Name;
            var dbContext = filterContext.HttpContext
            .RequestServices
            .GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            var AllRoleClaimsOfCurrentUser = (from cr in dbContext.RoleClaims
                                              join r in dbContext.UserRoles on cr.RoleId equals r.RoleId
                                              where cr.ClaimType == claimType && cr.ClaimValue == claimValue && r.UserId == dbContext.Users.FirstOrDefault(x => x.UserName == Username).Id
                                              select cr).Count();
            if (AllRoleClaimsOfCurrentUser > 0)
            {
                //base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new ForbidResult();
            }
        }
    }
}
