using System.ComponentModel.DataAnnotations;
using WebApi.Models.ModelsVM;

namespace WebApi.Controllers.Models
{
    //Đầu vào
    public class InputLoai : LoaiVM
    {

    }


    //DL trả về
    public class ResultLoai : LoaiVM
    {

    }

    /*public class LoaiVM
    {
        public int MaLoai { get; set; }

        public string TenLoai { set; get; }
    }*/
}
