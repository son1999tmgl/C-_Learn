using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Controllers.Data
{
    [Table("DonHang")]
    public class DTDonHang
    {
        public enum TinhTrangDatHang
        {
            New = 0,
            Payment = 1, 
            Complete = 2,
            Cancel = -1
        }

        public Guid MaDh { get; set; }

        public DateTime NgayDat { get; set; }

        public DateTime? NgayGiao { get; set; }

        public TinhTrangDatHang TinhTrangDonHang{ get; set; }

        public string NguoiNhan { get; set; }

        public string DiaChiGiao { get; set; }

        public string SoDienThoai { get; set; }


        public ICollection<DTDonHangChiTiet> DonHangChiTiets { get; set; }


        public DTDonHang()
        {
            DonHangChiTiets = new List<DTDonHangChiTiet>();
        }

    }
}
