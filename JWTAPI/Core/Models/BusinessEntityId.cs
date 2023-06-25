namespace JWTAPI.Core.Models
{
    public class BusinessEntityId
    {
        [Key]
        [Required]
        public string Name { get; set; }
        [Required]
        public string TypeDesc { get; set; }
    }
}
