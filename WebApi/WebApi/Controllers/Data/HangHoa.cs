using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Controllers.Data
{

    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        public Guid MaHh   { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string TenHh  { get; set; }

        public string MaTa { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }

        public byte GiamGia { get; set; }
    }
}
