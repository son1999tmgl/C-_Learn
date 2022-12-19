using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Controllers.Data
{

    [Table("HangHoa")]
    public class DTHangHoa
    {
        [Key]
        public Guid MaHh   { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string TenHh  { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }

        public byte? GiamGia { get; set; }
        
        //Mô tả
        public string MoTa { get; set; }

        public int? MaLoai {get; set; }

        [ForeignKey("MaLoai")]
        public DTLoai Loai { get; set; }


        public ICollection<DTDonHangChiTiet> DonHangChiTiets { get; set; }

        public DTHangHoa()
        {
            DonHangChiTiets = new HashSet<DTDonHangChiTiet>();
        }

    }
}
