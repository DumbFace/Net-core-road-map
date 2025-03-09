using Common.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetCoreAPI_Mongodb.AuthorizeFilter
{
    public class CustomAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly UserPermissionsEnum[] _permissions;

        //private readonly ILogger<CustomAuthorize> _logger;
        public CustomAuthorize(params UserPermissionsEnum[] permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var _logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<CustomAuthorize>>();


            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            string token = String.Empty;
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                token = authHeader.Substring("Bearer ".Length).Trim();
            }

            var userPermissions = ReadPermissions(token);

            if (!_permissions.Any(permission => userPermissions.Contains(permission)))
            {
                context.Result = new ForbidResult();
            }
        }

        public static UserPermissionsEnum[] ReadPermissions(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);



                var permissionAsString = jwtToken.Claims
                      .Where(c => c.Type == "Permissions")
                      .Select(c => c.Value)
                      .ToList();

                UserPermissionsEnum[] enumArray = permissionAsString
                      .Select(permission => (UserPermissionsEnum)Enum.Parse(typeof(UserPermissionsEnum), permission))
                      .ToArray();

                return enumArray;
            }

            return null;
        }
    }
}
