using Common.Enum;
using Common.Models.BaseModels;
using DnsClient.Internal;
using Infrastucture.AspnetCoreApi.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace Infrastucture.AspnetCoreApi.Services.Services
{
    public class JsonWebTokenService : IJsonWebTokenService
    {
        private readonly IConfiguration _configurationManager;
        private readonly ILogger<JsonWebTokenService> _logger;

        public JsonWebTokenService(IConfiguration configurationManager, ILogger<JsonWebTokenService> logger)
        {
            _configurationManager = configurationManager;
            _logger = logger;
        }

        public UserPermissionsEnum[] GetPermissionExample { get => [UserPermissionsEnum.WRITE, UserPermissionsEnum.READ]; }

        public string GenerateJwtToken(string userId, params UserPermissionsEnum[] userPermissionsAsEnums)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyConfigure = _configurationManager["Jwt:Key"];
            var audience = _configurationManager["Jwt:Audience"];
            var issuer = _configurationManager["Jwt:Issuer"];
            if (keyConfigure is null || audience is null || issuer is null)
            {
                throw new Exception("Cannt read configure");
            }

            var key = Encoding.ASCII.GetBytes(_configurationManager["Jwt:Key"]);

            //var claims = new Dictionary<string, object>();
            var listPermissionAsInt = new List<int>();  
            foreach (var permission in userPermissionsAsEnums)
            {
                listPermissionAsInt.Add((int)permission);
            }
            var claims = new Dictionary<string, object>
            {
                { "Permissions",listPermissionAsInt } 
            };
            _logger.LogInformation($"----------Key {key}");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Audience = audience,
                Issuer = issuer,
                Claims = claims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            //string path = "C:\\Users\\KangFarm\\bearer.txt";
            //string content = $"Authorization: Bearer {token}";
            //if (File.Exists(path))
            //    File.WriteAllText(path, content);


            return tokenHandler.WriteToken(token);
        }

        public UserModel GetLoginAccount(LoginModel login)
        {
            if (login is not null)
            {
                return new UserModel
                {
                    Id = 1,
                };
            }
            return null;
        }

    }
}
