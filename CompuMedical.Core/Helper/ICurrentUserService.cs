using System.Security.Claims;

namespace CompuMedical.Core.Helper;

public interface ICurrentUserService
{

    string CurrentUserId { get; }
    Claim[] FindClaims(string claimType);
    Claim[] GetAllClaims();
    bool IsInRole(string roleName);

}
