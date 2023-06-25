namespace JWTAPI.Controllers.Resources;
public class UserCredentialsResource
{
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(32)]
    public string Password { get; set; }

    [Required]
    [StringLength(255)]
    public string Username { get; set; }
    [StringLength(20)]
    public string Phone { get; set; }

    [Required]
    [StringLength(255)]
    public string RecoveryQuestion { get; set; }
    [Required]
    [StringLength(255)]
    public string RecoveryAnswer { get; set; }
}

public class UserLogin
{

    [Required]
    [StringLength(32)]
    public string Password { get; set; }

    [Required]
    [StringLength(255)]
    public string Username { get; set; }

}


public class ForgetPassword
{

    [Required]
    [StringLength(32)]
    public string NewPassword { get; set; }

    [Required]
    [StringLength(255)]
    public string Username { get; set; }
    [Required]
    [StringLength(255)]
    public string RecoveryQuestion { get; set; }
    [Required]
    [StringLength(255)]
    public string RecoveryAnswer { get; set; }

}