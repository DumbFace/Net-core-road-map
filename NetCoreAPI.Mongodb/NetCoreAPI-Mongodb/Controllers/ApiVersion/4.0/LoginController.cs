using Application.DTOs;
using Application.Interfaces;
using Common.Models.BaseModels;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI_Mongodb.Controllers.BaseController;

namespace NetCoreAPI_Mongodb.Controllers.ApiVersion.v4
{

    public class LoginController : BaseController_v4
    {
        private readonly IJsonWebTokenService _jsonWebTokenService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IJsonWebTokenService jsonWebTokenService, IMediator mediator) : base(mediator)
        {
            _jsonWebTokenService = jsonWebTokenService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        //[Consumes("application/json")]
        public IActionResult Login([FromBody] LoginModel login)
        {

            LoginDto loginDTO = new LoginDto
            {
                UserName = login.UserName,
                //Password = login.Pas
            };
            // Authenticate user
            var user = _jsonWebTokenService.GetLoginAccount(loginDTO);

            if (user == null)
                return Unauthorized();

            //var permissions = _jsonWebTokenService.GetPermissionExample;

            //var permissionAsString = JsonConvert.SerializeObject(permissions);

            //_logger.LogInformation($"----------------------------------permissionAsString: {permissionAsString}");


            //// Generate JWT token
            var token = _jsonWebTokenService.GenerateJwtToken(user.Id.ToString(), [UserPermissionsEnum.UPDATE, UserPermissionsEnum.WRITE]);
            string path = "C:\\Users\\KangFarm\\bearer.txt";
            string content = $"Authorization: Bearer {token}";
            if (System.IO.File.Exists(path))
                System.IO.File.WriteAllText(path, content);
            return Ok(new { token });
        }
    }
}
