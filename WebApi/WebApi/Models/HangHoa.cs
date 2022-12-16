using WebApi.Models.ModelsVM;

namespace WebApi.Controllers.Models
{

    //Model đầu vào
    public class InputHangHoa : HangHoaVM
    {
        

    }


    //Model đầu ra
    public class ResultHangHoa: HangHoaVM
    {
        public Guid MaHangHoa { get; set; }
        public string TenLoai { get; set; }

    }
}
