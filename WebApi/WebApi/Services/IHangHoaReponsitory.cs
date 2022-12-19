using WebApi.Controllers.Models;

namespace WebApi.Services
{
    public interface IHangHoaReponsitory
    {
        List<ResultHangHoa> GetAll(string search, int page, double? from, double? to, string? sortBy);

        ResultHangHoa Create(InputHangHoa hh);
    }
}
