using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data
{

    [Table("NguoiDung")]
    public class DTNguoiDung
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Password { get; set; }  
        
        public string HoTen { get; set; }

        public string Email { get; set; }
    }
}
