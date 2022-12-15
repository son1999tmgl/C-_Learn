using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.Models
{
    public class LoaiModel
    {

        [Required]
        [MaxLength(50)]
        public string TenLoai { get; set; } 
    }
}
