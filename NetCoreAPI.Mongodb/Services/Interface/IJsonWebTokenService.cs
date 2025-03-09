

using Common.Enum;
using Common.Models.BaseModels;

namespace Infrastucture.AspnetCoreApi.Services.Interface
{
    public interface IJsonWebTokenService
    {
        string GenerateJwtToken(string userId, params UserPermissionsEnum[] perrmisionsASString);


        UserModel GetLoginAccount(LoginModel login);

        UserPermissionsEnum[] GetPermissionExample { get; }
    }
}
