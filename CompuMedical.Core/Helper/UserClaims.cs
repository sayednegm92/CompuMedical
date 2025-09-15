namespace CompuMedical.Application.Helpers;

public class UserClaims
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}
