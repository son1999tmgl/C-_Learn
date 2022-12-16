using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.ModelsVM
{
    public class LoaiVM
    {
        public int MaLoai { get; set; }

        [Required]
        [MaxLength(50)]
        public string TenLoai { set; get; }
    }
}
