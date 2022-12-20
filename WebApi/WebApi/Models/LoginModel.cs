using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class LoginModel
    {

        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}
