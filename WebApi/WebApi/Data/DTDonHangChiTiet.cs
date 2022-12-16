namespace WebApi.Controllers.Data
{
    public class DTDonHangChiTiet
    {
        public Guid MaHh { get; set; }

        public Guid MaDh{ get; set; }

        public int SoLuong { get; set; }

        public double DonGia { get; set; }

        public byte GiamGia { get; set; }


        public DTDonHang DonHang { get; set; }

        public DTHangHoa HangHoa { get; set; }


    }
}
