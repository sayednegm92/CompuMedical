using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CompuMedical.Core.Helper;

public class CurrentUserService : ICurrentUserService
{
    #region Fields
    protected readonly IHttpContextAccessor _principalAccessor;
    private static readonly Claim[] EmptyClaimsArray = [];
    #endregion
    #region Constractor
    public CurrentUserService(IHttpContextAccessor principalAccessor)
    {
        _principalAccessor = principalAccessor;
    }
    #endregion
    #region Handle Functions

    public virtual string Email => _principalAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);


    public virtual string CurrentUserId
    {
        get
        {
            return _principalAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }

    public virtual Claim FindClaim(string claimType)
    {
        return _principalAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == claimType);
    }

    public virtual Claim[] FindClaims(string claimType)
    {
        return _principalAccessor.HttpContext.User.Claims?.Where(c => c.Type == claimType).ToArray() ?? EmptyClaimsArray;
    }

    public virtual Claim[] GetAllClaims()
    {
        return _principalAccessor.HttpContext.User.Claims?.ToArray() ?? EmptyClaimsArray;
    }

    public virtual bool IsInRole(string roleName)
    {
        return FindClaims("Role").Any(c => c.Value == roleName);
    }
    #endregion
}
