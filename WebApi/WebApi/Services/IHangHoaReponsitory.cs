using WebApi.Controllers.Models;

namespace WebApi.Services
{
    public interface IHangHoaReponsitory
    {
        List<ResultHangHoa> GetAll(string search, double? from, double? to, string? sortBy);
    }
}
