namespace JWTAPI.Controllers.Resources;
public class UserResource
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public IEnumerable<string> Roles { get; set; }
}