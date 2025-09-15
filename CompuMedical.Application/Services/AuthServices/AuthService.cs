using AutoMapper;
using CompuMedical.Application.Dto.Auth;

using CompuMedical.Application.Helper;
using CompuMedical.Application.Helpers;
using CompuMedical.Core.Entities;
using CompuMedical.Core.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompuMedical.Application.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _jwtSettings;
    private readonly IMapper _mapper;
    private readonly IResponseHandler _responseHandler;
    public AuthService(UserManager<ApplicationUser> userManager, IResponseHandler responseHandler, IMapper mapper, RoleManager<IdentityRole> roleManager, JwtOptions jwtSettings)
    {
        _userManager = userManager;
        _responseHandler = responseHandler;
        _jwtSettings = jwtSettings;
        _mapper = mapper;
        _roleManager = roleManager;

    }
    public async Task<GeneralResponse> Login(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        var userPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (user == null || !userPassword || !user.IsActive) return _responseHandler.ShowMessage("Username or Password May Be Wrong !");
        else
        {
            var Token = await CreateJWTToken(user);

            var Response = new UserResponseDto
            {
                Id = user.Id.ToString(),
                Username = user.UserName,
                Email = user.Email,
                Token = Token
            };
            return _responseHandler.Success(Response);

        }
    }
    public async Task<GeneralResponse> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return _responseHandler.ShowMessage("User not Found  !");
        else
        {

            var Response = new UserResponseDto
            {
                Id = user.Id.ToString(),
                Username = user.UserName,
                Email = user.Email,
            };
            return _responseHandler.Success(Response);

        }
    }
    public GeneralResponse GetAllUsers()
    {
        var users = _userManager.Users.Select(u => new { u.Id, u.UserName, u.Email }).ToList();

        if (users == null) return _responseHandler.ShowMessage("User not Found  !");
        return _responseHandler.Success(users);
    }
    public async Task<GeneralResponse> Register(UserRegisterDto request)
    {

        if (await _userManager.FindByNameAsync(request.UserName) is not null || await _userManager.FindByEmailAsync(request.Email) is not null)
            return _responseHandler.ShowMessage("Username already exists");
        var newUser = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
                errors += $"{error.Description},";
            return _responseHandler.ShowMessage(errors);
        }

        var roleAddition = await _userManager.AddToRoleAsync(newUser, "user");
        if (!roleAddition.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in roleAddition.Errors)
                errors += $"{error.Description},";
            return _responseHandler.ShowMessage(errors);
        }

        var Token = await CreateJWTToken(newUser);
        var Response = new UserResponseDto
        {
            Id = newUser.Id.ToString(),
            Username = newUser.UserName,
            Email = newUser.Email,
            Token = Token
        };

        return _responseHandler.Success(Response);
    }

    public async Task<GeneralResponse> EditUser(UserRegisterDto request)
    {
        var editUser = await _userManager.FindByNameAsync(request.UserName);
        if (editUser is null || await _userManager.FindByEmailAsync(request.Email) is null)
            return _responseHandler.ShowMessage("User not Found  !");

        var userUpdated = _mapper.Map(request, editUser);
        var result = await _userManager.UpdateAsync(userUpdated);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += $"{error.Description},";
            }
            return _responseHandler.ShowMessage(errors);
        }
        return _responseHandler.SuccessMessage("User Edit Successfully");
    }

    public async Task<GeneralResponse> ActivateUser(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) return _responseHandler.ShowMessage("User not Found  !");
        user.IsActive = true;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += $"{error.Description},";
            }
            return _responseHandler.ShowMessage(errors);
        }
        return _responseHandler.SuccessMessage("User Update Successfully");
    }

    public async Task<GeneralResponse> DeactivateUser(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) return _responseHandler.ShowMessage("User not Found  !");
        user.IsActive = false;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += $"{error.Description},";
            }
            return _responseHandler.ShowMessage(errors);
        }
        return _responseHandler.SuccessMessage("User Update Successfully");
    }

    public async Task<GeneralResponse> DeleteUser(string username)
    {
        var deleteUser = await _userManager.FindByNameAsync(username);
        if (deleteUser == null) return _responseHandler.ShowMessage("User not Found  !");
        var result = await _userManager.DeleteAsync(deleteUser);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += $"{error.Description},";
            }
            return _responseHandler.ShowMessage(errors);
        }
        else return _responseHandler.SuccessMessage("User Delete Successfully");
    }
    public async Task<GeneralResponse> ChangeUserPassword(string userName, string password, string newPassword)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return _responseHandler.ShowMessage("User not Found  !");
        var result = await _userManager.ChangePasswordAsync(user, password, newPassword);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += $"{error.Description},";
            }
            return _responseHandler.ShowMessage(errors);
        }
        return _responseHandler.SuccessMessage("User Update Successfully");
    }
    public async Task<GeneralResponse> AddRoleAsync(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (roleExist) return _responseHandler.SuccessMessage("Role already exists !");
        else
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        return _responseHandler.SuccessMessage("Role Added Successfully");
    }

    #region Base Method
    private async Task<string> CreateJWTToken(ApplicationUser user)
    {
        var claims = await GetClaims(user);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwtSettings.ExpirationInMinutes),
            signingCredentials: signingCredentials);

        var Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return Token;
    }
    private async Task<List<Claim>> GetClaims(ApplicationUser user)
    {
        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaims.UserId), user.Id.ToString())
            };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);
        return claims;
    }
    #endregion
}
