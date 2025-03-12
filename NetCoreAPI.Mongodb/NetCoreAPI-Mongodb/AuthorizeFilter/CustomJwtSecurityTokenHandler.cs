using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetCoreAPI_Mongodb.AuthorizeFilter
{
    public class CustomJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
       private TokenValidationParameters _validationParameters;
        private readonly IConfiguration _configurationManager;
        private readonly ILogger<CustomJwtSecurityTokenHandler> _logger;


        public CustomJwtSecurityTokenHandler(
            //ILogger<CustomJwtSecurityTokenHandler> logger,
            //IConfiguration configurationManager,
            //TokenValidationParameters validationParameters
            )
        {
            //_validationParameters = validationParameters;
            //_configurationManager = configurationManager; 
            //_logger = logger;
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters _validationParameters, out SecurityToken validatedToken)
        {
            //_logger.LogInformation($"---------------Configure JWT KEY {}")
            var principal = base.ValidateToken(token, _validationParameters, out validatedToken);

            if (principal.HasClaim(c => c.Type == "role" && c.Value == "banned"))
            {
                throw new SecurityTokenValidationException("User is banned");
            }

            return principal;
        }
    }
}
