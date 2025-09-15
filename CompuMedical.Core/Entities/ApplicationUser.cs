using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CompuMedical.Core.Entities;

public class ApplicationUser : IdentityUser
{
    [MaxLength(50)]
    public string? Address { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(100)]
    public string? FullName { get; set; }
    public bool IsActive { get; set; } = true;

}
