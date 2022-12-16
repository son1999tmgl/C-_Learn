 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Controllers.Data
{
    [Table("Loai")]
    public class DTLoai
    {
        [Key]
        public int MaLoai { get; set; }

        [Required]
        [MaxLength(50)]
        public string TenLoai { get; set; }


        public virtual ICollection<DTHangHoa> HangHoas { get; set; }  
 
    }
}
