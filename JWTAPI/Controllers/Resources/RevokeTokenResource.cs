namespace JWTAPI.Controllers.Resources;
public class RevokeTokenResource
{
    [Required]
    public string Token { get; set; }

    [Required]
    public string UserName { get; set; }
}