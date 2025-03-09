using Common.Enum;
using Common.Models.BaseModels;
using Infrastucture.AspnetCoreApi.Services.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI_Mongodb.AuthorizeFilter;
using NetCoreAPI_Mongodb.Controllers.BaseController;
using Newtonsoft.Json;

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

            //LoginModel login = new LoginModel
            //{
            //    UserName = username,
            //    Password = password
            //};
            // Authenticate user
            var user = _jsonWebTokenService.GetLoginAccount(login);

            if (user == null)
                return Unauthorized();

            //var permissions = _jsonWebTokenService.GetPermissionExample;

            //var permissionAsString = JsonConvert.SerializeObject(permissions);

            //_logger.LogInformation($"----------------------------------permissionAsString: {permissionAsString}");


            //// Generate JWT token
            var token = _jsonWebTokenService.GenerateJwtToken(user.Id.ToString(), [UserPermissionsEnum.UPDATE,UserPermissionsEnum.WRITE]);

            return Ok(new { token });
        }
    }
}
