using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
 
namespace SearchEngineAPI.Models.User
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }

    }
}
