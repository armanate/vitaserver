using System.Runtime.CompilerServices;

namespace JWTAPI.Core.Models;
public class User
{
    public long Id { get; set; }

    
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    
    public string Password { get; set; }

    [Required]
    [StringLength(255)]
    public string Username { get; set; }
    [StringLength(20)]
    public string Phone { get; set; }
    
    
    [StringLength(255)]
    public string RecoveryQuestion { get; set; }
    
    [StringLength(255)]
    public string RecoveryAnswer { get; set; }

    public DateTime DateCreated { get; set; }

    [StringLength(2048)]
    public string currentToken { get; set; }


    public ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
    public ICollection<Connected> Connected { get; set; }
    public ICollection<Document> Document { get; set; }
    public ICollection<Payments> Payments { get; set; }
}