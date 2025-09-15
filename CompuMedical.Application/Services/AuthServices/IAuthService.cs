
using CompuMedical.Application.Dto.Auth;
using CompuMedical.Application.Helper;

namespace CompuMedical.Application.Services.AuthServices;

public interface IAuthService
{
    Task<GeneralResponse> Login(LoginDto dto);
    Task<GeneralResponse> Register(UserRegisterDto request);
    Task<GeneralResponse> GetUserById(string userId);
    GeneralResponse GetAllUsers();
    Task<GeneralResponse> EditUser(UserRegisterDto request);
    Task<GeneralResponse> ActivateUser(string username);
    Task<GeneralResponse> DeactivateUser(string username);
    Task<GeneralResponse> DeleteUser(string username);
    Task<GeneralResponse> ChangeUserPassword(string userName, string password, string newPassword);
    Task<GeneralResponse> AddRoleAsync(string roleName);

}
