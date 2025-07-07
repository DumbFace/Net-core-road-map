using Application.DTOs;
using Common.Models.BaseModels;
using Domain.Enum;

namespace Application.Interfaces
{
    public interface IJsonWebTokenService
    {
        string GenerateJwtToken(string userId, params UserPermissionsEnum[] perrmisionsASString);

        UserModel GetLoginAccount(LoginDto login);

        UserPermissionsEnum[] GetPermissionExample { get; }
    }
}
