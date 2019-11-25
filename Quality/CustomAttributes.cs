using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System;
using Quality.Common.Helpers;
using System.Text;
using Quality.Common;

namespace Quality
{
    public class IsAdminAttribute : TypeFilterAttribute
    {
        public IsAdminAttribute() : base(typeof(ExecuteValidation))
        {

        }

        public class ExecuteValidation : IAuthorizationFilter
        {
            private readonly IClaimsHelper _claimsHelper;
            public ExecuteValidation(IClaimsHelper claimsHelper)
            {
                _claimsHelper = claimsHelper;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!_claimsHelper.IsAdmin)
                {
                    // status code: 401 unauthorized, 403 authorized but not allowed
                    context.HttpContext.Response.StatusCode = 403;
                    context.Result = new JsonResult(new Result
                    {
                        IsSuccess = false,
                        Message = "Not allowed"
                    });
                }
            }
        }
    }

    public class MyAuthorizeAttribute : TypeFilterAttribute
    {
        public MyAuthorizeAttribute() : base(typeof(ExecuteValidation))
        {

        }

        public class ExecuteValidation : IAuthorizationFilter
        {
            private readonly IClaimsHelper _claimsHelper;
            public ExecuteValidation(IClaimsHelper claimsHelper)
            {
                _claimsHelper = claimsHelper;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (_claimsHelper.UserId == 0)
                {
                    // status code: 401 unauthorized, 403 authorized but not allowed
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult(new Result
                    {
                        IsSuccess = false,
                        Message = "Unauthorize"
                    });
                }
            }
        }
    }

    public class RoleAttribute : TypeFilterAttribute
    {
        public RoleAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }

        public class ClaimRequirementFilter : IAuthorizationFilter
        {
            readonly Claim _claim;

            public ClaimRequirementFilter(Claim claim)
            {
                _claim = claim;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
                if (!hasClaim)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
