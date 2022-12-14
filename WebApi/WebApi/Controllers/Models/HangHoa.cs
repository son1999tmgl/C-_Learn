namespace WebApi.Controllers.Models
{
    public class HangHoaVM
    {
        public string TenHangHoa { get; set; }
        public string DonGia { get; set; }
    }

    public class HangHoa : HangHoaVM
    {
        public Guid MaHangHoa { get; set; }
    }
}
