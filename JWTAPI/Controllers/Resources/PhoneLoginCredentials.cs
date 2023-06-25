namespace JWTAPI.Controllers.Resources
{
    public class PhoneLoginCredentials
    {
        [StringLength(255)]
        public string Username { get; set; }
        [StringLength(20)]
        [Required]
        public string Phone { get; set; }

        
        public int? OtpCode { get; set; } = null;
    }
}
