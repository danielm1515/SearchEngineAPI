using System.ComponentModel.DataAnnotations;

namespace SearchEngineAPI.Models.Auth
{
    public class AuthUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
